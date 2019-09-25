using System;
using System.Collections.Generic;
using System.Text;
using Core.Common;
using Core.Persistence;

namespace Commands.DeleteProduct
{
    public class DeleteProductCommand : ICommand
    {
        IProductRepository _productRepository;

        public DeleteProductCommand(IProductRepository productRepository) {
            _productRepository = productRepository; 
        }

        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();

        public Guid ProductId { get; set; }


        public void Execute()
        {
            var product = _productRepository.GetProduct(ProductId);
            product.Deleted = true;
            _productRepository.UpdateProduct(product);
        }
    }
}
