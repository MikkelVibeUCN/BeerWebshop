using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces
{
    public interface IAccountAPIClient
    {
        Task<int> CreateAsync(CustomerDTO entity, string? endpoint = null);
        Task<bool> DeleteAsync(int id, string? endpoint = null);
        Task<IEnumerable<CustomerDTO>> GetAllAsync(string? endpoint = null);
        Task<CustomerDTO> GetCustomerByEmail(string email);

        Task<CustomerDTO?> GetAsync(int id, string? endpoint = null);
    }
}
