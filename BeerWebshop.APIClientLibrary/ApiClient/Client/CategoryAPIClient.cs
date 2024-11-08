using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class CategoryAPIClient : ICategoryAPIClient
{
	private RestClient _restClient;
	public CategoryAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));
	public async Task<int> CreateCategoryAsync(CategoryDTO categoryDTO)
	{
		var response = await _restClient.RequestAsync<int>(Method.Post, "Categories", categoryDTO);

		if (!response.IsSuccessful)
		{
			throw new Exception($"Error creating Brewery. Message was {response.Content}");
		}

		return response.Data;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var response = await _restClient.RequestAsync<bool>(Method.Delete, $"Categories/{id}");
		if (!response.IsSuccessful)
		{
			throw new Exception($"Error deleting Category. Message was {response.Content}");
		}
		return response.Data;
	}
}
