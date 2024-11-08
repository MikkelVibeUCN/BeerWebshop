using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class ProductAPIClient : IProductAPIClient
    {
        private RestClient _restClient;
        public ProductAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<int> CreateProductAsync(ProductDTO ProductDTO)
        {
            var response = await _restClient.RequestAsync<int>(Method.Post, "Products", ProductDTO);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating ProductDTO. Message was {response.Content}");
            }

            return response.Data;
        }
        public async Task<IEnumerable<string>> GetProductCategoriesAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<string>>(Method.Get, "Products/Categories");

            if(!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all categories. Message was {response.Content}");
            }

            return response.Data;
        }

        public async Task<ProductDTO?> GetProductFromIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<ProductDTO>(Method.Get, $"Products/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving ProductDTO. Message was {response.Content}");
            }
            return response.Data;
        }

        public Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
