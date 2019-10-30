using System;
using System.Collections.Generic;
using System.Text;
using Core.Common;
using Core.Persistence;
using Domain;
using NLog;
using ProductCatalog;

namespace Commands.DeleteProduct
{
    public class DeleteProductCommand : Command
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private IProductRepository _productRepository;
        private IEventRepository _eventRepository;

        public DeleteProductCommand(IProductRepository productRepository, IEventRepository eventRepository) {
            _productRepository = productRepository;
            _eventRepository = eventRepository;
        }


        public Guid ProductId { get; set; }


        public override void Execute()
        {
            var product = _productRepository.GetProduct(ProductId);
            product.Deleted = true;
            product.LastUpdate = DateTime.Now;
            _productRepository.UpdateProduct(product);
            _eventRepository.AddEvent(product.ConstructEvent(EventType.Deleted));

            Logger.Info("Product deleted: {@id}",ProductId);
        }

    }
}
