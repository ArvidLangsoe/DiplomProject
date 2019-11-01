using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            IConfiguration config = ConfigLoader.LoadConfig("appsettings.json");

            Logger.Debug("Starting app");
            var productCatalogClient = new ProductCatalogClient(config);

            var contextOptions = new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("StorageDb")).Options;
            var dbContext = new StorageDbContext(contextOptions);

            var productRepository = new AvailableProductRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var b = new ProductCacheManager(productCatalogClient,productRepository,unitOfWork);
            var listener = b.BeginEventListener();

            Logger.Debug("Event Listening begun.");
            
            Task.WaitAll(listener);

        }
    }
}

    