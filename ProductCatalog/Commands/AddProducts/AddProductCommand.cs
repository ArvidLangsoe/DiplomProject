using Commands.AddProducts;
using Core.Common;
using Core.Persistence;
using Domain;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.AddProducts
{
    public class AddProductCommand : ICommand
    {
        private IProductRepository _productRepository;
        private IEventRepository _eventRepository;

        public AddProductCommand(IProductRepository productRepository, IEventRepository eventRepository) {
            _productRepository = productRepository;
            _eventRepository = eventRepository;
        }

        public AddProductDTO ProductDTO { get; set; }
        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; }

        public void Execute()
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
            }

            _eventRepository.AddEvent(newProduct.ConstructEvent(EventType.Added));

        }
    }
}
