using Domain;
using Persistence;
using ProductCatalog.ProductCatalogClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class ProductCacheManager
    {
        private ProductCatalogClient _client;
        private AvailableProductRepository _productRepository;
        private UnitOfWork _unitOfWork;


        private bool _isListening = false;
        public bool IsListening { get {
                return _isListening;
            }
        }

        public ProductCacheManager(ProductCatalogClient client, AvailableProductRepository productRepository, UnitOfWork unitOfWork) {
            _client = client;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }


        public void  BeginEventListener(){
            _isListening = true;
            Task.Run(() =>Run());
        }

        public void StopEventListener()
        {
            _isListening = false;
        }

        private async void Run() {
            await _client.Authenticate();

            //Should be saved in database or file.
            int current = 0;
            while (_isListening) {
                IEnumerable<EventDTO> events = await _client.GetEvents(current, 50);
                if (events==null||events.Count() == 0) {
                    //TODO: Change this so that thread dont sleep. Not strictly needed but nice. 
                    //Events updated every 20 seconds
                    Thread.Sleep(20000);
                    continue;
                }
                current += events.Count();

                IEnumerable<EventDTO> productEvents = events.Where(x => x.ObjectType == "Product");
                IEnumerable<Guid> productsToRemove = events.Where(x => x.Event == "Deleted").Select(x => x.ObjectId);
                IEnumerable<Guid> productsToUpdate = events.Where(x => x.Event == "Updated"|| x.Event == "Added").Select(x => x.ObjectId).ToHashSet();

                await UpdateProductCache(productsToUpdate, productsToRemove);
            }
        }


        private async Task UpdateProductCache(IEnumerable<Guid> productsToUpdate, IEnumerable<Guid> productsToRemove =null) {
            IEnumerable<AvailableProduct> updatedProducts = await _client.GetProducts(productsToUpdate);

            foreach (AvailableProduct product in updatedProducts)
            {
                _productRepository.Replace(product);
            }

            IEnumerable<AvailableProduct> removedProducts = await _client.GetProducts(productsToUpdate);

            foreach (AvailableProduct product in removedProducts)
            {
                _productRepository.Replace(product);
            }

            _unitOfWork.CommitChanges();
        }


        

        

    }
}
