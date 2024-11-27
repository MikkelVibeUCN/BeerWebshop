using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.DesktopClient.Controllers;

public class OrderController
{
	private readonly IOrderApiClient _orderAPIClient;
	public string JwtToken { get; private set; }

    public OrderController(IOrderApiClient orderAPIClient, string? jwtToken = null)
	{
		_orderAPIClient = orderAPIClient;
        JwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));
    }

	public async Task<IEnumerable<OrderDTO>> GetAllOrders()
	{
		return await _orderAPIClient.GetAllAsync("Orders", JwtToken);
	}
}
