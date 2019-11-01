using Domain;
using Domain.Events;
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


        public DbSet<Batch> Batches { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }
        public DbSet<ProductAddition> AdditionEvents { get; set; }
        public DbSet<ProductRemoval> RemovalEvents { get; set; }

        public DbSet<AvailableProduct> AvailableProducts { get; set; }
        public DbSet<EventCounter> EventCounter { get; set; }


    }
}
