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
			CreatedAt = dto.Date,
			DeliveryAddress = dto.CustomerDTO?.Address,
			IsDelivered = dto.IsDelivered,
			CustomerId_FK = dto.CustomerDTO?.Id,
			OrderLines = (await Task.WhenAll(dto.OrderLines.Select(async dtoOrderLine =>
				new OrderLine
				{
					Quantity = dtoOrderLine.Quantity,
					Product = await MapToEntity(dtoOrderLine.Product, categoryService, breweryService, productService)
				}))).ToList()
		};
	}

	private static async Task<OrderLine> MapOrderLineDtoToEntity(OrderLineDTO dto, CategoryService categoryService, BreweryService breweryService, ProductService productService)
	{
		var product = await productService.GetProductByIdAsync(dto.Product.Id);
		return new OrderLine
		{
			Quantity = dto.Quantity,
			Product = product!
		};
	}


	public static OrderDTO MapOrderEntityToDTO(Order order)
	{
		return new OrderDTO
		{
			Id = order.Id ?? 0,
			Date = order.CreatedAt,
			IsDelivered = order.IsDelivered,
			OrderLines = order.OrderLines != null
				? order.OrderLines.Select(MapOrderLineEntityToDTO).ToList()
				: new List<OrderLineDTO>(),
			CustomerDTO = order.CustomerId_FK.HasValue ? new CustomerDTO
			{
				Id = order.CustomerId_FK.Value,
				Address = order.DeliveryAddress
			} : null
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

	private static async Task<Product> MapToEntity(ProductDTO productDTO, CategoryService categoryService, BreweryService breweryService, ProductService productService)
	{
		int? categoryId = await categoryService.GetCategoryIdByName(productDTO.CategoryName);
		if (categoryId == null) throw new Exception("Category not found");

		Category? category = await categoryService.GetCategoryById((int)categoryId);
		if (category == null) throw new Exception("Category not found");

		int? breweryId = await breweryService.GetBreweryIdByName(productDTO.BreweryName);
		if (breweryId == null) throw new Exception("Brewery not found");

		Brewery? brewery = await breweryService.GetBreweryById((int)breweryId);
		if (brewery == null) throw new Exception("Brewery not found");

		return new Product
		{
			Id = productDTO.Id,
			Name = productDTO.Name,
			Category = category,
			Brewery = brewery,
			Price = productDTO.Price,
			Description = productDTO.Description,
			Stock = productDTO.Stock,
			Abv = productDTO.ABV,
			ImageUrl = productDTO.ImageUrl,
			IsDeleted = false
		};
	}



}
