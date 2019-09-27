using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Persistence
{
    public interface IProductRepository
    {

        void AddProduct(Product product);
        ICatalog<Product> GetCatalog();
        Product GetProduct(Guid id);
        void UpdateProduct(Product currentProduct);
    }
}
