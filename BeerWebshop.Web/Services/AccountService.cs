using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class AccountService
    {
        private ICustomerpiClient _accountApiClient;

        public AccountService(IAccountAPIClient accountApiClient)
        {
            _accountApiClient = accountApiClient;
        }
        public async Task<CustomerDTO> CreateCustomer(CustomerDTO customer)
        {

        }
    }
}
