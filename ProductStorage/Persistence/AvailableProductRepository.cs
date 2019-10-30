﻿using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class AvailableProductRepository
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
                _dbContext.Update(product);
            }

        }

        public void Remove(Guid id)
        {
            var product = _dbContext.AvailableProducts.FirstOrDefault( x => x.Id == id);
            _dbContext.Remove(product);
        }

        public AvailableProduct Get(Guid id)
        {
            return _dbContext.AvailableProducts.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        
    }
}
