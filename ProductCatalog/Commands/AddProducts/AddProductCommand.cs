using Commands.AddProducts;
using Core.Persistence;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.AddProducts
{
    public class AddProductCommand : ICommand
    {
        private IProductRepository _productRepository;

        AddProductCommand(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public AddProductDTO ProductDTO { get; set; }
        public bool IsSuccesful { get; set; } = true; 

        public void Execute()
        {
            Product newProduct = new Product()
            {
                Id = new Guid(),
                Name = ProductDTO?.Name,
                Description = ProductDTO?.Description

            };

            try
            {
                _productRepository.AddProduct(newProduct);
            }
            catch (Exception) {
                //TODO: This could use a smarter implementation
                IsSuccesful = false;
            }

        }
    }
}
