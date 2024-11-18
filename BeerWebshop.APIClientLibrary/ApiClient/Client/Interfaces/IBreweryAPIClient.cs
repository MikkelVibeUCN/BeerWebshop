using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IBreweryAPIClient
{
    Task<int> CreateAsync(BreweryDTO entity, string? endpoint = null);
    Task<bool> DeleteAsync(int id, string? endpoint = null);
    Task<IEnumerable<BreweryDTO>> GetAllAsync(string? endpoint = null);
    Task<BreweryDTO?> GetAsync(int id, string? endpoint = null);
}
