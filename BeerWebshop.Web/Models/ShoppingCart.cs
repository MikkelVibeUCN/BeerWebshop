using BeerWebshop.Web.ApiClient.DTO;

namespace BeerWebshop.Web.Models
{
    public class ShoppingCart
    {
        public List<OrderLine> OrderLines { get; set; }

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
        public ShoppingCart(List<OrderLine> orderLines)
        {
            OrderLines = orderLines;
        }
        public ShoppingCart()
        {
            OrderLines = new List<OrderLine>();
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }
    }
}
