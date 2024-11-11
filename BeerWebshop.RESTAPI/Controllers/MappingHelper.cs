using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;

namespace BeerWebshop.RESTAPI.Controllers;

public static class MappingHelper
{
	public static async Task<Order> MapOrderDTOToEntity(OrderDTO dto, CategoryService categoryService, BreweryService breweryService, ProductService productService)
	{
		return new Order
		{
			Date = dto.Date,
			DeliveryAddress = dto.CustomerDTO.Address,
			IsDelivered = dto.IsDelivered,
			CustomerId_FK = dto.CustomerDTO.Id,
			OrderLines = (await Task.WhenAll(dto.OrderLines.Select(dto => MapOrderLineDtoToEntity(dto, categoryService, breweryService, productService)))).ToList()
		};
	}

	private static async Task<OrderLine> MapOrderLineDtoToEntity(OrderLineDTO dto, CategoryService categoryService, BreweryService breweryService, ProductService productService)
	{
		var product = await productService.GetProductByIdAsync(dto.Product.Id);
		return new OrderLine
		{
			ProductId = dto.Product.Id,
			Quantity = dto.Quantity,
			Product = product!
		};
	}

	public static OrderDTO MapOrderEntityToDTO(Order order)
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

	private static OrderLineDTO MapOrderLineEntityToDTO(OrderLine entity)
	{
		return new OrderLineDTO
		{
			Quantity = entity.Quantity,
			Product = MapToDTO(entity.Product)
		};
	}

	private static ProductDTO MapToDTO(Product product)
	{
		return new ProductDTO
		{
			Id = product.Id ?? 0,
			Name = product.Name,
			BreweryName = product.Brewery?.Name!,
			Price = product.Price,
			Description = product.Description,
			Stock = product.Stock,
			ABV = product.Abv,
			CategoryName = product.Category?.Name!,
			ImageUrl = product.ImageUrl!
		};
	}
}
