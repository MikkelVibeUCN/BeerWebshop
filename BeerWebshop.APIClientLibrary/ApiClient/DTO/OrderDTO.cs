namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Customer Customer { get; set; }
        public bool IsDelivered { get; set; }

        public float TotalPrice
        {
            get
            {
                if(OrderLines.Count == 0)
                {
                    return 0;
                }
                return OrderLines.Sum(ol => ol.SubTotal);
            }
        }

        public OrderDTO(DateTime date, List<OrderLine> orderLines, string deliveryAddress, bool isDelivered)
        {
            Date = date;
            OrderLines = orderLines;
            IsDelivered = isDelivered;
        }

        public Order()
        {
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
}
