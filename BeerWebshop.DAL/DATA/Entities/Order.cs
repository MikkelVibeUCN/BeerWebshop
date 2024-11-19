namespace BeerWebshop.DAL.DATA.Entities;

public class Order
{
	public DateTime CreatedAt { get; set; }
	public List<OrderLine> OrderLines { get; set; }
	public string? DeliveryAddress { get; set; }
	public bool IsDelivered { get; set; }
	public int? Id { get; set; }
	public bool IsDeleted { get; set; }
	public Customer Customer { get; set; }

    public float TotalPrice
	{
		get
		{
			return OrderLines.Sum(ol => ol.SubTotal);
		}
	}
	public Order() { }

	public Order(DateTime createdAt, List<OrderLine> orderLines, bool isDelivered, Customer customer, string? deliveryAddress = null, int? id = null)
	{
		CreatedAt = createdAt;
		OrderLines = orderLines;
		DeliveryAddress = deliveryAddress;
		IsDelivered = isDelivered;
		Id = id;
		Customer = customer;

	}

	public void AddOrderLine(OrderLine orderLine)
	{
		OrderLines.Add(orderLine);
	}
	public void RemoveOrderLine(OrderLine orderLine)
	{
		OrderLines.Remove(orderLine);
	}

}
