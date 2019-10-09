using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class ProductCatalogClient
    {
        private HttpClient _client = new HttpClient();
        private string Uri;

        
        public ProductCatalogClient() {
            //Config help: https://garywoodfine.com/configuration-api-net-core-console-application/
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",false,true);
            IConfiguration config = builder
                .Build();

            Uri = config["Uri:ProductCatalog"]; 
        }


        public async Task GetEvents()
        {
            var path = Uri + "/api/event?eventCounter=0&amount=10";
            HttpResponseMessage response = await _client.GetAsync(path);
            string responseString = await response.Content.ReadAsStringAsync();
            object obj = JsonConvert.DeserializeObject(responseString);


            //TODO: Deserialize, make a list of ids that changed and remove the ones that were deleted.

        }

    }

}
