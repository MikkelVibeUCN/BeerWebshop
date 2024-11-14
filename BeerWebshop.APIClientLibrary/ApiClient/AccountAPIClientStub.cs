using BeerWebshop.APIClientLibrary.ApiClient.Client;
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

        public Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            if (email == TestEmail)
            {
                return Task.FromResult(new CustomerDTO
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
                return Task.FromResult<CustomerDTO>(null);
            }
        }
    }
}
