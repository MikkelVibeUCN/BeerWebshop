﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BeerWebshop.RESTAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly OrderService _orderService;
		private readonly ProductService _productService;
		private readonly CategoryService _categoryService;
		private readonly BreweryService _breweryService;
		private readonly AccountService _accountService;

		public OrdersController(OrderService orderService, ProductService productService, CategoryService categoryService, BreweryService breweryService, AccountService accountService)
		{
			_orderService = orderService;
			_productService = productService;
			_categoryService = categoryService;
			_breweryService = breweryService;
            _accountService = accountService;
        }

		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult> GetOrderByIdAsync(int id)
		{
            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Not logged in");
            }
            try
			{
				var order = await _orderService.GetOrderByIdAsync(id);

                if (order == null)
                {
                    return NotFound();

                }

                if (!order.Customer.Email.Equals(email))
                {
                    return Unauthorized("Not authorized to view this order");
                }

                var orderDTO = MappingHelper.MapOrderEntityToDTO(order);

				return Ok(orderDTO);
			}
			catch (Exception ex)
			{

				return StatusCode(statusCode: 500, ex.Message);
			}
		}

		[HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersAsync()
		{
			try
			{
				var ordersDtos = await _orderService.GetOrdersAsync();
				return Ok(ordersDtos);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("LoggedInOrders")]
		[Authorize(Policy = "UserOnly")]
		public async Task<ActionResult<IEnumerable<OrderDTO>>> GetLoggedInCustomersOrders()
		{
            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;


            if (string.IsNullOrEmpty(email)) 
			{
				return Unauthorized("Not logged in");
			}

            Customer? customer = await _accountService.GetByEmail(email);

            if (customer == null)
            {
                return BadRequest("Customr not found");
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByCustomerIdAsync((int)customer.Id);

				return Ok(orderDtos);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var orderId = await _orderService.CreateOrderFromDTOAsync(dto);
				return Ok(orderId);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteOrderAsync(int id)
		{
			try
			{
				var order = await _orderService.GetOrderByIdAsync(id);
				if (order == null)
					return NotFound();

				await _orderService.DeleteOrderByIdAsync(id);
				return Ok(true);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}
	}
}
