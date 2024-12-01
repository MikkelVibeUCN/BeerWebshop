using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Cookies
{
    public class CartCookie
    {
        public List<OrderLineCutDown> OrderLines { get; set; }

        public static implicit operator CartCookie(ShoppingCart v)
        {
            throw new NotImplementedException();
        }
    }

    public class OrderLineCutDown
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}