using Application.Interfaces.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class BatchRepository : IBatchRepository
    {

        private StorageDbContext _dbContext;

        public BatchRepository(StorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBatch(Batch batch)
        {
            _dbContext.Batches.Add(batch);   
        }
    }
}
