using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Cookies
{
    public class ShoppingCart
    {
        public List<OrderLineDTO> OrderLines { get; set; }

        public float TotalPrice
        {
            get
            {
                if (OrderLines.Count == 0)
                {
                    return 0;
                }
                return OrderLines.Sum(ol => ol.SubTotal);
            }
        }
        public ShoppingCart(List<OrderLineDTO> orderLines)
        {
            OrderLines = orderLines;
        }
        public ShoppingCart()
        {
            OrderLines = new List<OrderLineDTO>();
        }

        public void AddOrderLine(OrderLineDTO OrderLineDTO)
        {
            OrderLines.Add(OrderLineDTO);
        }
    }
}
