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
using System.Net;
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
        private IConfiguration _config;

        private string AccessToken;


        public ProductCatalogClient(IConfiguration config)
        {
            _config = config;

            Uri = _config["Uri:ProductCatalog"];

        }

        public async Task  Authenticate() {
            await RetrieveAuthToken();
        }

        private async Task RetrieveAuthToken()
        {
            var response = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _config["Auth0:Address"],
                ClientId = _config["Auth0:ClientId"],
                ClientSecret = _config["Auth0:ClientSecret"],
                Parameters = _config.GetSection("Auth0:Parameters").Get<Dictionary<string, string>>(),
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
            HasAccess(response);
            string responseString = await response.Content.ReadAsStringAsync();
            List<EventDTO> events = JsonConvert.DeserializeObject<List<EventDTO>>(responseString);
            return events;
        }

        public async Task<List<AvailableProduct>> GetProducts(IEnumerable<Guid> ProductIds)
        {
            var path = Uri + "/api/product/Specific";
            string reqBody = JsonConvert.SerializeObject(ProductIds).ToString();
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(reqBody, Encoding.Default, "application/json"));
            HasAccess(response);
            string resContentString = await response.Content.ReadAsStringAsync();
            dynamic resContent = JsonConvert.DeserializeObject(resContentString);

            string productsString = JsonConvert.SerializeObject(resContent["items"]);
            var products = JsonConvert.DeserializeObject<List<AvailableProduct>>(productsString);

            return products;
        }

        private bool HasAccess(HttpResponseMessage response) {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AccessDeniedException("Acces to the service was denied.");
            }

            return true;
        } 

    }

}
