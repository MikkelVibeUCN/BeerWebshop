namespace BeerWebshop.Web.ApiClient.DTO
{
    public class Order
    {
        public DateTime Date { get; set; }
        private List<OrderLine> OrderLines { get; set; }
        public string DeliveryAddress { get; set; }
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

        public Order(DateTime date, List<OrderLine> orderLines, string deliveryAddress, bool isDelivered)
        {
            Date = date;
            OrderLines = orderLines;
            DeliveryAddress = deliveryAddress;
            IsDelivered = isDelivered;
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
