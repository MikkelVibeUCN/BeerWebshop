using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

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

		public Task EditProductAsync(ProductDTO product)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<string>> GetProductCategoriesAsync()
		{
			var response = await _restClient.RequestAsync<IEnumerable<string>>(Method.Get, "Products/Categories");

			if (!response.IsSuccessful || response.Data == null)
			{
				throw new Exception($"Error retrieving all categories. Message was {response.Content}");
			}

			return response.Data ?? Enumerable.Empty<string>();
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

		public async Task<ProductDTO?> GetProductFromIdAsync(int id)
		{
			var response = await _restClient.RequestAsync<ProductDTO>(Method.Get, $"Products/{id}");

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


		public async Task<bool> DeleteProductByIdAsync(int id)
		{
			var response = await _restClient.RequestAsync<bool>(Method.Delete, $"Products/{id}");

			if (!response.IsSuccessful)
			{
				throw new Exception($"Error deleting ProductDTO. Message was {response.Content}");
			}
			return response.Data;
		}
	}
}
