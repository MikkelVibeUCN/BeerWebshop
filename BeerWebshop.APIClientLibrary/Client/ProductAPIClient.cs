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
                throw new Exception($"Error retrieving all beers. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
        public Task<IEnumerable<Product>> GetAllBeersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> CreateBeerAsync(ProductDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateBeerAsync(ProductDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBeerAsync(int id)
        {
            throw new NotImplementedException();
        }



    }
}
