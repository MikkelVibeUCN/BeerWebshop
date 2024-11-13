using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class CategoryAPIClient : BaseClient<CategoryDTO>, ICategoryAPIClient
{
	public CategoryAPIClient(string uri) : base(uri, "Categories") { }
}