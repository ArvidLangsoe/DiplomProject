using Application.Interfaces.Persistence;
using Domain;
using Domain.Events;
using Persistence;
using ProductCatalog.ProductCatalogClient;
using ProductCatalogWatcher.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class ProductCacheManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string EVENT_COUNTER_ID = "Product Catalog";
        private static readonly int DELAY_SECONDS = 20;
        private static readonly int MILISECONDS_PER_SECONDS = 1000;

        private ProductCatalogClient _client;
        private IAvailableProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;


        private bool _isListening = false;
        public bool IsListening { get {
                return _isListening;
            }
        }

        public ProductCacheManager(ProductCatalogClient client, IAvailableProductRepository productRepository, IUnitOfWork unitOfWork) {
            _client = client;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }


        public Task  BeginEventListener(){
            _isListening = true;
            return Task.Run(() =>Run());
        }

        public void StopEventListener()
        {
            _isListening = false;
        }

        private async Task Run() {
            await _client.Authenticate();

            EventCounter eventCounter = _productRepository.GetEventCounter(EVENT_COUNTER_ID);

            if (eventCounter == null) {

                _productRepository.AddEventCounter(new EventCounter() { Id= EVENT_COUNTER_ID, Value=0});
                _unitOfWork.CommitChanges();
                eventCounter = _productRepository.GetEventCounter(EVENT_COUNTER_ID);
            }
            while (_isListening) {
                try
                {
                    IEnumerable<EventDTO> events = await _client.GetEvents(eventCounter.Value, 50);
                    if (events == null || events.Count() == 0)
                    {
                        //TODO: Change this so that thread dont sleep. Not strictly needed but nice. 
                        //Events updated every 20 seconds
                        Logger.Debug("No product catalog events found. Waiting {seconds} seconds before next update.", DELAY_SECONDS);
                        Thread.Sleep(DELAY_SECONDS * MILISECONDS_PER_SECONDS);
                        continue;
                    }

                    Logger.Debug("Product events received");
                    IEnumerable<EventDTO> productEvents = events.Where(x => x.ObjectType == "Product");
                    IEnumerable<Guid> productsToRemove = events.Where(x => x.Event == "Deleted").Select(x => x.ObjectId).ToHashSet();
                    IEnumerable<Guid> productsToUpdate = events.Where(x => x.Event == "Updated" || x.Event == "Added").Select(x => x.ObjectId).ToHashSet();


                    await UpdateProductCache(productsToUpdate, productsToRemove);
                    eventCounter.Value += events.Count();
                    _unitOfWork.CommitChanges();

                    Logger.Info("Product changes applied to: {@ids}", productsToRemove.Concat(productsToUpdate).ToHashSet());
                }
                catch (AccessDeniedException)
                {
                    Logger.Info("Access denied to Product catalog. Retrying.");
                    await _client.Authenticate();
                }
                catch (HttpRequestException) {
                    Logger.Warn("Cannot connect to ProductCatalog. Waiting before retry.");
                    Thread.Sleep(DELAY_SECONDS * MILISECONDS_PER_SECONDS);
                }
            }
        }


        private async Task UpdateProductCache(IEnumerable<Guid> productsToUpdate, IEnumerable<Guid> productsToRemove =null) {
            IEnumerable<AvailableProduct> updatedProducts = await _client.GetProducts(productsToUpdate);

            foreach (AvailableProduct product in updatedProducts)
            {
                _productRepository.Replace(product);
            }
            Logger.Debug("Products updated: {@ids}", productsToUpdate);


            foreach (Guid productId in productsToRemove)
            {
                _productRepository.Remove(productId);
            }

            Logger.Debug("Products removed: {@ids}", productsToRemove);
        }


        

        

    }
}
