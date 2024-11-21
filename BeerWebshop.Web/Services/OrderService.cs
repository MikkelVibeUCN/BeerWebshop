﻿using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;
using BeerWebshop.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
			return await _orderAPIClient.GetAsync(id);
		}

		public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId)
		{
			return await _orderAPIClient.GetOrdersByCustomerIdAsync(customerId);
			
		}

		public async Task<int> SaveOrder(CheckoutViewModel checkout)
		{
			if(checkout.Customer == null)
			{
                checkout.Customer = CreateCustomerFromCheckout(checkout.Checkout);
            }

			OrderDTO orderDTO = new OrderDTO
			{
				CustomerDTO = checkout.Customer,
				OrderLines = checkout.Cart.OrderLines,
				Date = DateTime.Now,
			};

			return await _orderAPIClient.CreateAsync(orderDTO);
		}

		private CustomerDTO CreateCustomerFromCheckout(Checkout checkout)
		{
			// TODO: Add email and password to this
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
