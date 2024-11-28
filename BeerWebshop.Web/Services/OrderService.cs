using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;
using BeerWebshop.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Data;

namespace BeerWebshop.Web.Services
{
    public class OrderService
	{
		private readonly IOrderApiClient _orderAPIClient;
		public OrderService(IOrderApiClient orderAPIClient)
		{
			_orderAPIClient = orderAPIClient;
		}

		public async Task<OrderDTO?> GetOrderFromId(int id, string token)
		{
			return await _orderAPIClient.GetAsync(id, null, token);
		}

		public async Task<IEnumerable<OrderDTO>> GetLoggedInCustomerOrders(string jwtToken)
		{
			return await _orderAPIClient.GetLoggedInCustomerOrders(jwtToken);
		}

		public async Task<int> SaveOrder(CheckoutViewModel checkout)
		{
            OrderDTO orderDTO = new OrderDTO
			{
				CustomerDTO = checkout.Customer,
				OrderLines = checkout.Cart.OrderLines,
				Date = DateTime.Now,
			};

			return await _orderAPIClient.CreateAsync(orderDTO);
		}
	}
}