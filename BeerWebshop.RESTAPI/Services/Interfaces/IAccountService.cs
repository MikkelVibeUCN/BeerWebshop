using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<int> SaveCustomerAsync(Customer customer);
        Task<Account?> GetByEmail(string email);
        Task DeleteCustomer(int id);
        Task<string?> AuthenticateAndGetTokenAsync(LoginViewModel loginViewModel);
    }
}
