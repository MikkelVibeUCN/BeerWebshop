using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Tools;

internal static class MappingHelper
{

	public static Order MapOrderDTOToEntity(OrderDTO dto, List<OrderLine> orderLines)
	{
		Customer customer = MapToEntity(dto.CustomerDTO);

        return new Order
		{
			CreatedAt = dto.Date,
			DeliveryAddress = dto.CustomerDTO?.Address,
			IsDelivered = dto.IsDelivered,
			Customer = customer,
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
			CustomerDTO = MapToDTO(order.Customer)
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
			RowVersion = product.RowVersion != null ? Convert.ToBase64String(product.RowVersion) : string.Empty
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
			IsDeleted = false,
			RowVersion = Convert.FromBase64String(productDTO.RowVersion)
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

	public static CategoryDTO MapBreweryEntityToDTO(Brewery brewery)
	{
		return new CategoryDTO
		{
			Id = brewery.Id,
			Name = brewery.Name
		};
	}
	public static Customer MapToEntity(CustomerDTO customer)
	{
		return new Customer
		{
			Id = customer.Id??0,
            Name = customer.Name,
			Address = customer.Address,
			Email = customer.Email,
			Phone = customer.Phone,
			Password = customer.Password,
			Age = customer.Age,
        };
	}
	public static CustomerDTO MapToDTO(Customer customer)
	{
		return new CustomerDTO
		{
			Id = customer.Id,
			Name = customer.Name,
			Address = customer.Address,
			Email = customer.Email,
			Phone = customer.Phone,
			Password = customer.Password,
			Age = customer.Age,
		};
	}
}
