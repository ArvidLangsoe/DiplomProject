using Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductCatalog.ProductCatalogClient;
using ProductCatalogWatcher.ProductCatalog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogWatcher
{
    public class ProductCatalogClient
    {
        private HttpClient _client = new HttpClient();
        private string Uri;
        private IConfiguration config;

        private string AuthToken;


        public ProductCatalogClient()
        {
            //Config help: https://garywoodfine.com/configuration-api-net-core-console-application/
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            config = builder
                .Build();

            Uri = config["Uri:ProductCatalog"];

            RetrieveAuthToken();
        }

        private void RetrieveAuthToken()
        {
            var path = "https://dev-akl.eu.auth0.com/oauth/token/";
            var clientCredentials = new ClientCredentials();
            var client = new RestClient(path);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type","client_credentials");
            request.AddParameter("client_id","NHYaSGobpFhgVmDyAuXJfrI7JkwgFyZq");
            request.AddParameter("client_secret","cKIAshV8eiyWwtcB4elVXQV6S52is6IiP5zngOtNurvcovQ6s4WbwoV6etm-mOwx");
            request.AddParameter("audience","productcatalog.kappelhoj.com");
            IRestResponse response = client.Execute(request);
            dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
            AuthToken = responseContent["access_token"];
        }

        public async Task<List<EventDTO>> GetEvents(int eventCounter, int amount)
        {

            var path = Uri + "/api/event?eventCounter=" + eventCounter + "&amount=" + amount;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
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

            //TODO Finish this method/ client in general its uglish
        }



    }

}
