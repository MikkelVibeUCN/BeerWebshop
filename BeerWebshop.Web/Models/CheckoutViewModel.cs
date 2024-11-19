using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Models
{
    public class CheckoutViewModel
    {
        public Checkout Checkout { get; set; }
        public ShoppingCart Cart { get; set; }
        public CustomerDTO? Customer { get; set; }
    }
}
