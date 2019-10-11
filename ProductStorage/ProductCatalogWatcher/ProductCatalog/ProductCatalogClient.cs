using Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductCatalog.ProductCatalogClient;
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


        public async Task<List<EventDTO>> GetEvents(int eventCounter, int amount)
        {
            var path = Uri + "/api/event?eventCounter="+eventCounter+"&amount="+ amount;
            HttpResponseMessage response = await _client.GetAsync(path);
            string responseString = await response.Content.ReadAsStringAsync();
            List<EventDTO> events = JsonConvert.DeserializeObject<List<EventDTO>>(responseString);
            return events;
        }

        public async Task GetProducts(IEnumerable<Guid> ProductIds)
        {
            var path = Uri + "/api/product/Specific";
            string content = JsonConvert.SerializeObject(ProductIds).ToString();
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(content, Encoding.Default, "application/json"));
            string responseString = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(responseString);
            
        }

    }

}
