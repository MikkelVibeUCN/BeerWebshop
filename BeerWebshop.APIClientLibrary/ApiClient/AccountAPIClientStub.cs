using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient
{
    public class AccountAPIClientStub : IAccountAPIClient
    {
        public Task<int> CreateAsync(CustomerDTO entity, string? endpoint = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateCustomer(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, string? endpoint = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDTO>> GetAllAsync(string? endpoint = null)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO?> GetAsync(int id, string? endpoint = null)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO?> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
