namespace BeerWebshop.Web.Models
{
    public class Checkout
    {
            public string Name { get; set; }
            public string Address { get; set; }
            public ShoppingCart Cart { get; set; }
        }
    }
