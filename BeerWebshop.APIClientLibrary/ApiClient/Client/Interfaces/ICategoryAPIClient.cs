using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces
{
    public interface ICategoryAPIClient
    {
        Task<int> CreateAsync(CategoryDTO entity, string? endpoint = null);
        Task<bool> DeleteAsync(int id, string? endpoint = null);
        Task<IEnumerable<CategoryDTO>> GetAllAsync(string? endpoint = null);
        Task<CategoryDTO?> GetAsync(int id, string? endpoint = null);
    }
}
