using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;

namespace BeerWebshop.Web.Models
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

        public CartCookie ConvertToCookie() => new CartCookie
        {
            OrderLines = OrderLines.Select(ol => new OrderLineCutDown
            {
                ProductId = ol.Product.Id,
                Quantity = ol.Quantity
            }).ToList()
        };
    }
}
