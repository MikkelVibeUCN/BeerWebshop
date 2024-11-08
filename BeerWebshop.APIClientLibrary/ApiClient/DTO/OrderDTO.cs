namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
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

        public OrderDTO(DateTime date, List<OrderLineDTO> orderLines, string deliveryAddress, bool isDelivered)
        {
            Date = date;
            OrderLines = orderLines;
            IsDelivered = isDelivered;
        }

        public OrderDTO()
        {
        }

        public void AddOrderLine(OrderLineDTO OrderLineDTO)
        {
            OrderLines.Add(OrderLineDTO);
        }
        public void RemoveOrderLine(OrderLineDTO OrderLineDTO)
        {
            OrderLines.Remove(OrderLineDTO);
        }
    }
}
