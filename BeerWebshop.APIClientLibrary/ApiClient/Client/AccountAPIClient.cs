using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class AccountAPIClient : BaseClient<CustomerDTO>, IAccountAPIClient
{
    public AccountAPIClient(string uri) : base(uri, "accounts")
    {
    }

    public async Task<CustomerDTO?> GetAsync(string jwtToken, string? endpoint = null)
    {
        RestRequest request = new RestRequest("accounts", Method.Get);
        request.AddHeader("Authorization", $"Bearer {jwtToken}");

        var response = await _restClient.ExecuteAsync<CustomerDTO?>(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error fetching token message was: {response.Content}");
        }
        return response.Data;
    }


    public async Task<string?> GetLoginToken(LoginViewModel viewModel)
    {
        var response = await _restClient.RequestAsync<TokenResponse?>(Method.Post, "accounts/customerlogin", viewModel);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error fetching token message was: {response.Content}");
        }
        return response.Data.Token;
    }

    public async Task<string?> CreateAsync(AccountCreationViewModel viewModel)
    {
        var response = await _restClient.RequestAsync<TokenResponse?>(Method.Post, "accounts/register", viewModel);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error creating account message was: {response.Content}");
        }
        return response.Data.Token;
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
