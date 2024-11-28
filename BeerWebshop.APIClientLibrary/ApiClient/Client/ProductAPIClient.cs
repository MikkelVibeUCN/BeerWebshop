using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class ProductAPIClient : BaseClient<ProductDTO>, IProductAPIClient
    {
        public ProductAPIClient(string uri) : base(uri, "Products") { }

        public async Task EditProductAsync(ProductDTO product, string? jwtToken = null)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            var endpoint = $"Products/{product.Id}";
            var request = new RestRequest(endpoint, Method.Put);

            request.AddJsonBody(product);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error updating ProductDTO with ID {product.Id}. Message was: {response.Content}");
            }
        }

        public async Task<IEnumerable<string>> GetProductCategoriesAsync()
        {
            return await GetAllAsync<string>($"{_defaultEndPoint}/Categories");
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

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters, string? jwtToken = null)
        {
            var request = new RestRequest("products", Method.Get);
            request.AddJsonBody(parameters);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<IEnumerable<ProductDTO>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error retrieving Products. Message was {response.Content}");
            }

            return response.Data ?? Enumerable.Empty<ProductDTO>();
        }


    }
}
