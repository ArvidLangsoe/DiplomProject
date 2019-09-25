using System;
using System.Collections.Generic;
using System.Text;
using Core.Common;
using Core.Persistence;
using Domain;
using ProductCatalog;

namespace Commands.DeleteProduct
{
    public class DeleteProductCommand : ICommand
    {
        private IProductRepository _productRepository;
        private IEventRepository _eventRepository;

        public DeleteProductCommand(IProductRepository productRepository, IEventRepository eventRepository) {
            _productRepository = productRepository;
            _eventRepository = eventRepository;
        }

        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();

        public Guid ProductId { get; set; }


        public void Execute()
        {
            var product = _productRepository.GetProduct(ProductId);
            product.Deleted = true;
            product.LastUpdate = DateTime.Now;
            _productRepository.UpdateProduct(product);
            _eventRepository.AddEvent(product.ConstructEvent(EventType.Deleted));
        }

    }
}
