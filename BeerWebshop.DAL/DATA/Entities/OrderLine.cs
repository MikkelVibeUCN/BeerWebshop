namespace BeerWebshop.DAL.DATA.Entities;

public class OrderLine
{
	public int Quantity { get; set; }
	public Product Product { get; set; }

	public float SubTotal
	{
		get
		{
			return Quantity * (Product?.Price ?? 0);
		}

	}

	public OrderLine() { }

	public OrderLine(int quantity, Product product)
	{
		Quantity = quantity;
		Product = product;

	}

}
