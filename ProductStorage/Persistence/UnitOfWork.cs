using Application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private StorageDbContext _dbContext;

        public UnitOfWork(StorageDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void CommitChanges()
        {

            _dbContext.SaveChanges();
        }
    }
}
