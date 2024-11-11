using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

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

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDTO>> GetOrderByIdAsync(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order == null)
				return NotFound();

			var orderDTO = MappingHelper.MapOrderEntityToDTO(order);
			return Ok(orderDTO);
		}

		[HttpPost]
		public async Task<ActionResult<int>> CreateOrderAsync([FromBody] OrderDTO dto)
		{
			var order = await MappingHelper.MapOrderDTOToEntity(dto, _categoryService, _breweryService, _productService);
			var orderId = await _orderService.CreateOrderAsync(order);
			return Ok(orderId);
		}
	}
}
