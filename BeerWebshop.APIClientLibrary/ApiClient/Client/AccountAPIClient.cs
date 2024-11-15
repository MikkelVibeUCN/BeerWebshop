using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class AccountAPIClient : BaseClient<CustomerDTO>, IAccountAPIClient
{

    public AccountAPIClient(string uri) : base(uri, "account")
    {
    }
    public async Task<CustomerDTO> GetCustomerByEmail(string email)
    {

        var response = await _restClient.RequestAsync<CustomerDTO>(Method.Get, "customer", email);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error retrieving ProductDTO. Message was {response.Content}");
        }
        return response.Data;

        //var request = new RestRequest("api/account/customer", Method.Get);
        //request.AddQueryParameter("email", email);
        //var response = await _restClient.ExecuteAsync<CustomerDTO>(request);
        //return response.Data;

    }

    public Task<string> GetHashedPasswordAsync(string email)
	{
		throw new NotImplementedException();
	}
}
