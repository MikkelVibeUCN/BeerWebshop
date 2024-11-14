using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.DesktopClient.Controllers;

public class OrderController
{
	private readonly IOrderApiClient _orderAPIClient;

	public OrderController(IOrderApiClient orderAPIClient)
	{
		_orderAPIClient = orderAPIClient;
	}

	public async Task<IEnumerable<OrderDTO>> GetAllOrders()
	{
		return await _orderAPIClient.GetAllOrdersAsync();
	}
}
