using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class AccountAPIClient : BaseClient<CustomerDTO>, IAccountAPIClient
{

    public AccountAPIClient(string uri) : base(uri, "account")
    {
    }
    public async Task<CustomerDTO?> GetCustomerByEmail(string email)
    {
        return await GetByString(email);
    }

    public Task<string> GetHashedPasswordAsync(string email)
	{
		throw new NotImplementedException();
	}
}
