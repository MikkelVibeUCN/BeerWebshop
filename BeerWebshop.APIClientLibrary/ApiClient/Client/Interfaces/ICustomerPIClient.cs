using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IAccountAPIClient
{
	Task<int> CreateCustomer(CustomerDTO customer);
	Task<CustomerDTO?> GetCustomer(int id);
}
