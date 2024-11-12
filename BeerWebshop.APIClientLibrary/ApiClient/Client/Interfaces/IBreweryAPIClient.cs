using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IBreweryAPIClient
{
	Task<int> CreateBreweryAsync(BreweryDTO brewery);
	Task<bool> DeleteAsync(int id);
}
