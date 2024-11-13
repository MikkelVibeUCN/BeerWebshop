using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class AccountService
    {
        private IAccountAPIClient _customerApiClient;

        public AccountService(IAccountAPIClient customerApiClient)
        {
            _customerApiClient = customerApiClient;
        }

        public async Task<int> CreateCustomerAsync(CustomerDTO customer)
        {
            return await _customerApiClient.CreateAsync(customer);
        }
    }
}
