using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IOrderApiClient
{
    Task<int> CreateAsync(OrderDTO entity, string? endpoint = null);
    Task<bool> DeleteAsync(int id, string? endpoint = null);
    Task<IEnumerable<OrderDTO>> GetAllAsync(string? endpoint = null);
    Task<OrderDTO?> GetAsync(int id, string? endpoint = null);
}
