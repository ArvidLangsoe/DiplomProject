using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
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

            while (_isListening) {

                //TODO: Get Events update products.

            }
        }


        

        

    }
}
