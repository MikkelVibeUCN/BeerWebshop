using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces
{
    public interface IAccountAPIClient
    {
        Task<string?> CreateAsync(AccountCreationViewModel model);
        Task<int> CreateAsync(CustomerDTO entity, string? endpoint = null, string? jwtToken = null);
        Task<bool> DeleteAsync(int id, string? endpoint = null, string? jwtToken = null);
        Task<IEnumerable<CustomerDTO>> GetAllAsync(string? endpoint = null, string? jwtToken = null);
        Task<AccountDTO?> GetAsync(string jwtToken, string? endpoint = null);
        Task<string?> GetLoginToken(LoginViewModel viewModel);
    }
}