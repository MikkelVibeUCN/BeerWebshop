using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IBreweryAPIClient
{
    Task<int> CreateAsync(CategoryDTO entity, string? endpoint = null);
    Task<bool> DeleteAsync(int id, string? endpoint = null);
    Task<IEnumerable<CategoryDTO>> GetAllAsync(string? endpoint = null);
    Task<CategoryDTO?> GetAsync(int id, string? endpoint = null);
}
