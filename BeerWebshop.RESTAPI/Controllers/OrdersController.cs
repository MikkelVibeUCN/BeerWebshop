using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BeerWebshop.RESTAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderDAO _orderDao;
		private readonly IProductDAO _productDao;

		public OrdersController(IOrderDAO orderDao, IProductDAO productDao)
		{
			_orderDao = orderDao;
			_productDao = productDao;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDTO>> GetOrderByIdAsync(int id)
		{
			var order = await _orderDao.GetByIdAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			var orderDTO = MapOrderEntityToDTO(order);
			return Ok(orderDTO);
		}

		[HttpPost]
		public async Task<ActionResult<int>> CreateOrderAsync([FromBody] OrderDTO dto)
		{
			var order = await MapOrderDTOToEntity(dto);
			var orderId = await _orderDao.InsertCompleteOrderAsync(order);
			return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = orderId }, orderId);
		}

		// Map OrderDTO to Order Entity
		private async Task<Order> MapOrderDTOToEntity(OrderDTO dto)
		{
			return new Order
			{
				Date = dto.Date,
				DeliveryAddress = dto.CustomerDTO.Address,
				IsDelivered = dto.IsDelivered,
				CustomerId_FK = dto.CustomerDTO.Id,
				OrderLines = (await Task.WhenAll(dto.OrderLines.Select(MapOrderLineDtoToEntity))).ToList()
			};
		}

		// Map Order Entity til OrderDTO
		private OrderDTO MapOrderEntityToDTO(Order order)
		{
			return new OrderDTO
			{
				Id = order.Id ?? 0,
				Date = order.Date,
				IsDelivered = order.IsDelivered,
				OrderLines = order.OrderLines.Select(MapOrderLineEntityToDTO).ToList(),
				CustomerDTO = new CustomerDTO
				{
					Id = order.CustomerId_FK ?? 0,
					Address = order.DeliveryAddress
				}
			};
		}

		// Map OrderLineDTO til OrderLine Entity
		private async Task<OrderLine> MapOrderLineDtoToEntity(OrderLineDTO dto)
		{
			return new OrderLine
			{
				ProductId = dto.Product.Id,
				Quantity = dto.Quantity,
				Product = await MapToEntity(dto.Product)
			};
		}

		// Map OrderLine Entity til OrderLineDTO
		private OrderLineDTO MapOrderLineEntityToDTO(OrderLine entity)
		{
			return new OrderLineDTO
			{
				Quantity = entity.Quantity,
				Product = MapToDTO(entity.Product)
			};
		}
		//TODO : Skulle måske overveje at flytte mappings til en helper klasse

		// Map ProductDTO til Product Entity
		private async Task<Product> MapToEntity(ProductDTO productDTO)
		{
			return new Product
			{
				Name = productDTO.Name,
				CategoryId_FK = await _productDao.GetCategoryIdByName(productDTO.Type),
				BreweryId_FK = await _productDao.GetBreweryIdByName(productDTO.Brewery),
				Price = productDTO.Price,
				Description = productDTO.Description,
				Stock = productDTO.Stock,
				Abv = productDTO.ABV,
				ImageUrl = productDTO.ImageUrl,
				IsDeleted = false
			};
		}

		// Map Product Entity til ProductDTO
		private ProductDTO MapToDTO(Product product)
		{
			return new ProductDTO
			{
				Id = product.Id ?? 0,
				Name = product.Name,
				Brewery = product.Brewery?.Name,
				Price = product.Price,
				Description = product.Description,
				Stock = product.Stock,
				ABV = product.Abv,
				Type = product.Category?.Name,
				ImageUrl = product.ImageUrl
			};
		}
	}
}
