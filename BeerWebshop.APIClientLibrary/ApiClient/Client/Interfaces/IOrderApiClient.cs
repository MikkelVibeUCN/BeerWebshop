using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IOrderApiClient
{
	Task<int> SaveOrder(OrderDTO Order);
	Task<OrderDTO?> GetOrderFromId(int id);
	Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
}
