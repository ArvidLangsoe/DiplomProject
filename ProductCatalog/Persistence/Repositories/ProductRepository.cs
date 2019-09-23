using Core.Persistence;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductDbContext _dbContext;

        ProductRepository(ProductDbContext dbContext) {
            _dbContext = dbContext;
        }


        public void AddProduct(Product product)
        {
            _dbContext.Add(product);
        }
    }
}
