using BeerWebshop.APIClientLibrary.ApiClient.DTO;
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

		[HttpGet("{id}", Name = "GetOrderId")]
		[Authorize]
		public async Task<ActionResult> GetOrderByIdAsync(int id)
		{
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Not logged in");
            }

            try
			{
				var orders = await _orderService.GetOrdersByCustomerIdAsync(_accountService.GetByEmail(email).Id);

                if (!orders.Any(o => o.Id == id))
                {
                    return Unauthorized("Not authorized to view this order");
                }

                var order = await _orderService.GetOrderByIdAsync(id);
				if (order == null)
				{
                    return NotFound();

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
		[Authorize]
		public async Task<ActionResult<IEnumerable<OrderDTO>>> GetLoggedInCustomersOrders()
		{
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

			if(string.IsNullOrEmpty(email)) 
			{
				return Unauthorized("Not logged in");
			}

            try
			{
				var customerId = _accountService.GetByEmail(email).Id;

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
