﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

		public OrdersController(OrderService orderService, ProductService productService, CategoryService categoryService, BreweryService breweryService)
		{
			_orderService = orderService;
			_productService = productService;
			_categoryService = categoryService;
			_breweryService = breweryService;
		}

		[HttpGet("{id}", Name = "GetOrderId")]
		public async Task<ActionResult> GetOrderByIdAsync(int id)
		{
			try
			{
				var order = await _orderService.GetOrderByIdAsync(id);
				if (order == null)
					return NotFound();

				var orderDTO = MappingHelper.MapOrderEntityToDTO(order);
				return Ok(orderDTO);
			}
			catch (Exception ex)
			{

				return StatusCode(statusCode: 500, ex.Message);
			}
		}

		[HttpGet]
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

		[HttpGet("{customerId}/orders")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<OrderDTO>>> GetLoggedInCustomersOrders()
		{

			try
			{
				var ordersDtos = await _orderService.GetOrdersByCustomerIdAsync(customerId);
				return Ok(ordersDtos);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
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
