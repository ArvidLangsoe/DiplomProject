using System;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
           var a =  new ProductCatalogClient();


            Task.WaitAll(a.GetEvents());
        }
    }
}
