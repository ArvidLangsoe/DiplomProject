using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }

        public new void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Order> OrderedProducts { get; set; }
    }
}
