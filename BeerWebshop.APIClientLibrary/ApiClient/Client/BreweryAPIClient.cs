using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class BreweryAPIClient : IBreweryAPIClient
{
	private RestClient _restClient;
	public BreweryAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));
	public async Task<int> CreateBreweryAsync(BreweryDTO breweryDTO)
	{
		var response = await _restClient.RequestAsync<int>(Method.Post, "Breweries", breweryDTO);

		if (!response.IsSuccessful)
		{
			throw new Exception($"Error creating Brewery. Message was {response.Content}");
		}

		return response.Data;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var response = await _restClient.RequestAsync<bool>(Method.Delete, $"Breweries/{id}");
		if (!response.IsSuccessful) {
			throw new Exception($"Error deleting Brewery. Message was {response.Content}");
		}
		return response.Data;
	}
}
