using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private OrderDbContext _context;

        public UnitOfWork(OrderDbContext context) {
            _context = context;

        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }
    }
}
