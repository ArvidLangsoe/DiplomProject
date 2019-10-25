using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Common;
using Core.Persistence;
using Core.Util.PatchProperty;
using Domain;
using ProductCatalog;

namespace Commands.UpdateProducts
{
    public class UpdateProductCommand : Command
    {
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
                IsSuccesful = false;
                return;
            }
            if (currentProduct.Deleted)
            {
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

        }


        private void PatchProperty(PropertyUpdate propertyUpdate, Product productToPatch)
        {
            try
            {
                propertyUpdate.PatchProperty(productToPatch);
            }
            catch (ArgumentException e)
            {
                IsSuccesful = false;

                Errors.Add(new Error()
                {
                    Message = "The given value does not match the expected type for property \"" + propertyUpdate.Property 
                    + "\". This most likely happened because a string was supplied instead."
                });
            }
        }

    }
}
