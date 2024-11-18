using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class ProductAPIClient : BaseClient<ProductDTO>, IProductAPIClient
    {
        public ProductAPIClient(string uri) : base(uri, "Products") { }

        public async Task EditProductAsync(ProductDTO product)
        {
            var response = await _restClient.RequestAsync(Method.Put, $"Products/{product.Id}", product);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error updating ProductDTO with ID{product.Id}. Message was {response.Content}");
            }
        }
        public async Task<IEnumerable<string>> GetProductCategoriesAsync()
        {
            return await GetAllAsync<string>("Products/Categories");
        }

        public async Task<int> GetProductCountAsync(ProductQueryParameters parameters)
        {
            var response = await _restClient.RequestAsync<int>(Method.Get, "products/count", parameters);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving ProductDTO. Message was {response.Content}");
            }
            return response.Data;

        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var response = await _restClient.RequestAsync<IEnumerable<ProductDTO>>(Method.Get, "products", parameters);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error retrieving Products. Message was {response.Content}");
            }

            return response.Data ?? Enumerable.Empty<ProductDTO>();
        }


    }
}
