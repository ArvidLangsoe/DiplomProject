using Application.Interfaces.Persistence;
using Domain;
using Domain.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class AvailableProductRepository : IAvailableProductRepository
    {

        private StorageDbContext _dbContext;

        public AvailableProductRepository(StorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public void Replace(AvailableProduct product) {
            var stored = _dbContext.AvailableProducts.FirstOrDefault(x => x.Id == product.Id);

            if (stored == null)
            {
                _dbContext.Add(product);
            }
            else
            {
                stored.Discontinued = product.Discontinued;
                stored.Title = product.Title;
            }

        }

        public void Remove(Guid id)
        {
            var product = _dbContext.AvailableProducts.FirstOrDefault( x => x.Id == id);
            if (product == null) {
                return;
            }
            _dbContext.Remove(product);
        }

        public AvailableProduct Get(Guid id)
        {
            return _dbContext.AvailableProducts.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public EventCounter GetEventCounter(string id)
        {
            return _dbContext.EventCounter.FirstOrDefault(x => x.Id == id);
        }

        public void AddEventCounter(EventCounter eventCounter)
        {
            _dbContext.Add(eventCounter);
        }
    }
}
