using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
	public interface IAccountAPIClient
	{
		Task<CustomerDTO> GetCustomerByEmail(string email);

	}
}
