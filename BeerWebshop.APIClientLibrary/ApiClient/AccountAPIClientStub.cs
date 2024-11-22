using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient
{
    public class AccountApiClientStub : IAccountAPIClient
    {
        private const string TestEmail = "test@example.com";
        private const string TestPassword = "password123";
        private readonly string _hashedPassword;

        public AccountApiClientStub()
        {
            _hashedPassword = BCrypt.Net.BCrypt.HashPassword(TestPassword);
        }

        public Task<string?> CreateAsync(AccountCreationViewModel model, string? endpoint = null, string? jwtToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<string?> CreateAsync(AccountCreationViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(CustomerDTO entity, string? endpoint = null, string? jwtToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, string? endpoint = null, string? jwtToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDTO>> GetAllAsync(string? endpoint = null, string? jwtToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO?> GetAsync(string jwtToken, string? endpoint = null)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO?> GetByEmailAsync(string email)
        {
            if (email == TestEmail)
            {
                return Task.FromResult<CustomerDTO?>(new CustomerDTO
                {
                    Id = 1,
                    Email = TestEmail,
                    Password = _hashedPassword,
                    Name = "Test User",
                    Address = "123 Test St",
                    Phone = "123-456-7890",
                    Age = 30
                });
            }
            else
            {
                return Task.FromResult<CustomerDTO?>(null);
            }
        }

        public Task<string?> GetLoginToken(LoginViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
