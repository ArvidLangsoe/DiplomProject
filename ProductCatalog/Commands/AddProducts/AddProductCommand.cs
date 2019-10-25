using Commands.AddProducts;
using Core.Common;
using Core.Persistence;
using Domain;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Commands.AddProducts
{
    public class AddProductCommand : Command
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private IProductRepository _productRepository;
        private IEventRepository _eventRepository;

        public AddProductCommand(IProductRepository productRepository, IEventRepository eventRepository) {
            _productRepository = productRepository;
            _eventRepository = eventRepository;
        }

        public AddProductDTO ProductDTO { get; set; }

        public override void Execute()
        {
            Product newProduct = new Product()
            {
                Id = new Guid(),
                Title = ProductDTO?.Title,
                Description = ProductDTO?.Description

            };

            try
            {
                _productRepository.AddProduct(newProduct);
            }
            catch (Exception) {
                //TODO: This could use a smarter implementation, we dont want to catch all exceptions.
                IsSuccesful = false;
                Logger.Warn("Failed to add product to database. There might be an issue with the database connection. Product: {@Product} ", newProduct);
            }
            
            _eventRepository.AddEvent(newProduct.ConstructEvent(EventType.Added));
            Logger.Info("Product Added: {@Product} ", newProduct);
        }
    }
}
