using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Tools;

public static class MappingHelper
{
	public static Order MapOrderDTOToEntity(OrderDTO dto, List<OrderLine> orderLines)
	{
		return new Order
		{
			CreatedAt = dto.Date,
			DeliveryAddress = dto.CustomerDTO?.Address,
			IsDelivered = dto.IsDelivered,
			CustomerId_FK = dto.CustomerDTO?.Id,
			OrderLines = orderLines
		};
	}

	public static OrderLine MapOrderLineDtoToEntity(OrderLineDTO dto, Product product)
	{
		return new OrderLine
		{
			Quantity = dto.Quantity,
			Product = product
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

	public static OrderLineDTO MapOrderLineEntityToDTO(OrderLine entity)
	{
		return new OrderLineDTO
		{
			Quantity = entity.Quantity,
			Product = MapProductEntityToDTO(entity.Product)
		};
	}


	public static ProductDTO MapProductEntityToDTO(Product product)
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
			ImageUrl = product.ImageUrl!,
			RowVersion = product.RowVersion
		};
	}

	public static Product MapProductDTOToEntity(ProductDTO productDTO, Category category, Brewery brewery)
	{
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

	public static Category MapCategoryDTOToEntity(CategoryDTO categoryDTO)
	{
		return new Category
		{
			Name = categoryDTO.Name,
			IsDeleted = false
		};
	}

	public static Brewery MapBreweryDTOToEntity(BreweryDTO breweryDTO)
	{

		return new Brewery
		{
			Name = breweryDTO.Name,
			IsDeleted = false
		};
	}

	public static BreweryDTO MapBreweryEntityToDTO(Brewery brewery)
	{
		return new BreweryDTO
		{
			Id = brewery.Id,
			Name = brewery.Name
		};
	}
}
