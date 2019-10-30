using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Common;
using Core.Persistence;
using Core.Util.PatchProperty;
using Domain;
using NLog;
using ProductCatalog;

namespace Commands.UpdateProducts
{
    public class UpdateProductCommand : Command
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IProductRepository _productRepository;
        private IEventRepository _eventRepository;

        public UpdateProductCommand(IProductRepository productRepository, IEventRepository eventRepository)
        {
            _productRepository = productRepository;
            _eventRepository = eventRepository;
        }

        public UpdateProductDTO ProductUpdate { get; set; }

        public override void Execute()
        {
            Product currentProduct = _productRepository.GetProduct(ProductUpdate.Id);
            if (currentProduct == null)
            {
                Logger.Info("Update Attempt, could not find product: {@id}", ProductUpdate.Id);
                IsSuccesful = false;
                return;
            }
            if (currentProduct.Deleted)
            {
                Logger.Info("Update Attempt, product is deleted: {@id}", ProductUpdate.Id);
                IsSuccesful = false;
                return;
            }

            foreach (PropertyUpdate propertyUpdate in ProductUpdate.Updates)
            {
                PatchProperty(propertyUpdate, currentProduct);
            }
            if (!IsSuccesful)
            {
                return;
            }
            currentProduct.LastUpdate = DateTime.Now;
            _productRepository.UpdateProduct(currentProduct);
            _eventRepository.AddEvent(currentProduct.ConstructEvent(EventType.Updated));
            Logger.Info("Product updated {@id}", ProductUpdate.Id);
        }


        private void PatchProperty(PropertyUpdate propertyUpdate, Product productToPatch)
        {
            try
            {
                propertyUpdate.PatchProperty(productToPatch);
            }
            catch (ArgumentException)
            {
                IsSuccesful = false;

                Error propertyError = new Error()
                {
                    Message = "The given value does not match the expected type for property \"" + propertyUpdate.Property
                    + "\". This most likely happened because a string was supplied instead."
                };
                Logger.Warn("Update Attempt, could not patch property: {property}", propertyUpdate.Property);
                Logger.Warn(propertyError.Message);

                Errors.Add(propertyError);
            }
        }

    }
}
