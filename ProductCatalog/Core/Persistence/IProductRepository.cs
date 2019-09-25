using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Persistence
{
    public interface IProductRepository
    {

        void AddProduct(Product product);
        IEnumerable<Product> GetProducts();
        Product GetProduct(Guid id);
    }
}
