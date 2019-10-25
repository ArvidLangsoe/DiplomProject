using System;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Hello world");
            var b = new ProductCacheManager(null,null,null);
            b.BeginEventListener();

            var a =  new ProductCatalogClient();
            var guids = new Guid[] { new Guid("64ff24d8-fcb7-4120-bb2f-08d741bcd130"),
                new Guid("315b7d9f-c2ea-4da7-42fb-08d7432e95a8")
            };

            a.Authenticate();
            Task.WaitAll(a.GetEvents(0, 10));
            Task.WaitAll(a.GetProducts(guids));
        }
    }
}

    