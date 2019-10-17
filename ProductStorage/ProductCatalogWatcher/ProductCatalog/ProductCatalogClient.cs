using Domain;
using IdentityModel.Client;
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

        private string AccessToken;


        public ProductCatalogClient()
        {
            //Config help: https://garywoodfine.com/configuration-api-net-core-console-application/
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            config = builder
                .Build();

            Uri = config["Uri:ProductCatalog"];

        }

        public async void  Authenticate() {
            await RetrieveAuthToken();
        }

        private async Task RetrieveAuthToken()
        {
            var clientCredentials = new ClientCredentials();
            var response = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://dev-akl.eu.auth0.com/oauth/token/",
                ClientId = "NHYaSGobpFhgVmDyAuXJfrI7JkwgFyZq",
                ClientSecret = "cKIAshV8eiyWwtcB4elVXQV6S52is6IiP5zngOtNurvcovQ6s4WbwoV6etm-mOwx",
                Parameters = {
                    { "audience", "productcatalog.kappelhoj.com"}
                }

            });
            if (response.IsError) {
                throw new ApplicationException("Could not connect to Auth0");
            }
            AccessToken = response.AccessToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        public async Task<List<EventDTO>> GetEvents(int eventCounter, int amount)
        {

            var path = Uri + "/api/event?eventCounter=" + eventCounter + "&amount=" + amount;
            HttpResponseMessage response = await _client.GetAsync(path);
            string responseString = await response.Content.ReadAsStringAsync();
            List<EventDTO> events = JsonConvert.DeserializeObject<List<EventDTO>>(responseString);
            return events;
        }

        public async Task<List<AvailableProduct>> GetProducts(IEnumerable<Guid> ProductIds)
        {
            var path = Uri + "/api/product/Specific";
            string reqBody = JsonConvert.SerializeObject(ProductIds).ToString();
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(reqBody, Encoding.Default, "application/json"));
            string resContentString = await response.Content.ReadAsStringAsync();
            dynamic resContent = JsonConvert.DeserializeObject(resContentString);

            string productsString = JsonConvert.SerializeObject(resContent["items"]);
            var products = JsonConvert.DeserializeObject<List<AvailableProduct>>(productsString);

            return products;
        }



    }

}
