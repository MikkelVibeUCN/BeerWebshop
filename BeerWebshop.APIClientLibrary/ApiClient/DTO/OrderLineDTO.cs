namespace BeerWebshop.APIClientLibrary.ApiClient.DTO;

public class OrderLineDTO
{
	public int Quantity { get; set; }
	public ProductDTO Product { get; set; }

	public float SubTotal
	{
		get
		{
			return Quantity * Product.Price;
		}
	}

	public OrderLineDTO() { }
	public OrderLineDTO(int quantity, ProductDTO product)
	{
		Quantity = quantity;
		Product = product;
	}
}
