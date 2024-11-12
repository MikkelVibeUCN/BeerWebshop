using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class AccountService
    {
        private ICustomerAPIClient _customerApiClient;

        public AccountService(ICustomerAPIClient customerApiClient)
        {
            _customerApiClient = customerApiClient;
        }

        public async Task<int> CreateCustomerAsync(CustomerDTO customer)
        {
            return await _customerApiClient.CreateCustomer(customer);
        }
    }
}
