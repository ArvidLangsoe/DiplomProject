using Application.Interfaces.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class ProductStorageRepository : IProductStorageRepository
    {

        private StorageDbContext _dbContext;

        public ProductStorageRepository(StorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductStorage GetProductStorage(Guid productId) {

            var productBatches = _dbContext.ProductBatches.Where(x => x.ProductId == productId);
            var additionEvents = _dbContext.AdditionEvents.Where(x => x.ProductBatch.ProductId == productId);
            var removalEvents = _dbContext.RemovalEvents.Where(x => x.ProductBatch.ProductId == productId);

            var productStorage = new ProductStorage(productBatches,additionEvents,removalEvents);

            return productStorage;
        }

    }
}
