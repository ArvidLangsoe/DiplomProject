using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Common;
using Core.Persistence;
using ProductCatalog;

namespace Commands.UpdateProducts
{
    public class UpdateProductCommand : ICommand
    {
        private IProductRepository _productRepository;

        public UpdateProductCommand(IProductRepository productRepository) {
            _productRepository = productRepository;
        }


        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();
        public UpdateProductDTO ProductUpdate { get; set; }



        public void Execute()
        {
            Product currentProduct = _productRepository.GetProduct(ProductUpdate.Id);
            if (currentProduct == null) {
                IsSuccesful = false;
                return;
            }
            PatchProduct(currentProduct);
        }



        private void PatchProduct(Product currentProduct) {

            PatchProperty(ProductUpdate.Name, currentProduct, x => currentProduct.Name);
            PatchProperty(ProductUpdate.Description, currentProduct, x => currentProduct.Description);
            PatchProperty(ProductUpdate.Discontinued, currentProduct, x => currentProduct.Discontinued);
        }

        private void PatchProperty<T,V>(T input, V target, Expression<Func<V,T>> propertyToChange) {
            if (input != null) {
                var expr = (MemberExpression)propertyToChange.Body;
                var prop = (PropertyInfo)expr.Member;

                prop.SetValue(target, input, null);
            }

        }
    }
}
