using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services
{
	public interface IOrderService
	{
		Task<int> CreateOrderAsync(Order order);
		Task<Order> GetOrderByIdAsync(int id);
	}
}
