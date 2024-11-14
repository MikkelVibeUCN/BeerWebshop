using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class AccountAPIClient : IAccountAPIClient
{
	private RestClient _restClient;
	public AccountAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));

    public Task<CustomerDTO> GetCustomerByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetHashedPasswordAsync(string email)
	{
		throw new NotImplementedException();
	}
}
