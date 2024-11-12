using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
	public class OrderService
	{
		private readonly IOrderApiClient _orderAPIClient;
		public OrderService(IOrderApiClient orderAPIClient)
		{
			_orderAPIClient = orderAPIClient;
		}

		public async Task<OrderDTO?> GetOrderFromId(int id)
		{
			return await _orderAPIClient.GetOrderFromId(id);
		}

		public async Task<int> SaveOrder(Checkout checkout, ShoppingCart cart)
		{
			CustomerDTO customerDTO = CreateCustomerFromCheckout(checkout);

			OrderDTO orderDTO = new OrderDTO
			{
				CustomerDTO = customerDTO,
				OrderLines = cart.OrderLines,
				Date = DateTime.Now,
			};

			return await _orderAPIClient.SaveOrder(orderDTO);
		}

		private CustomerDTO CreateCustomerFromCheckout(Checkout checkout)
		{
			string name = checkout.Firstname + " " + checkout.Lastname;

			string address = checkout.Street + " " + checkout.Number + ", " + checkout.PostalCode + " " + checkout.City;

			return new CustomerDTO
			{
				Name = name,
				Address = address,
				Phone = checkout.Phonenumber
			};
		}

	}
}
