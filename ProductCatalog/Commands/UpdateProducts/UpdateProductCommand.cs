using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Common;
using Core.Persistence;
using Core.Util.PatchProperty;
using ProductCatalog;

namespace Commands.UpdateProducts
{
    public class UpdateProductCommand : ICommand
    {
        private IProductRepository _productRepository;

        public UpdateProductCommand(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();
        public UpdateProductDTO ProductUpdate { get; set; }



        public void Execute()
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

            _productRepository.UpdateProduct(currentProduct);
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
