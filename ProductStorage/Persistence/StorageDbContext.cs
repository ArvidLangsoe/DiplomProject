using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions options) : base(options)
        {

        }

        public new void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        public DbSet<ProductBatch> ProductBatches { get; set; }
        public DbSet<ProductAddition> AdditionEvents { get; set; }
        public DbSet<ProductRemoval> RemovalEvents { get; set; }


    }
}
