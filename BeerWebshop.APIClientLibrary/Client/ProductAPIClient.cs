using BeerWebshop.APIClientLibrary.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.Client
{
    public class ProductAPIClient : IProductAPIClient
    {
        private RestClient _restClient;
        public ProductAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<Product> GetBeerByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<Product>(Method.Get, $"Products/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving the beer by its id. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<Product>> GetAllBeersAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<Product>>(Method.Get, $"Products");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all beers. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<Product>> GetBeerByCategory(string category)
        {
            
            var response = await _restClient.RequestAsync<List<Product>>(Method.Get, $"Products/category/{category}");

            if (!response.IsSuccessful)
            {
                
                throw new Exception($"Error retrieving beers by category '{category}'. Status Code: {response.StatusCode}, Message: {response.ErrorMessage}");
            }

            return response.Data ?? new List<Product>(); 
        }

    }
}
