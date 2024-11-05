using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public interface ICartService
    {

        ShoppingCart GetCart();
        void RemoveFromCart(int productId);

        void UpdateQuantity(int productId, int quantity);

        void AddToCart(int productId, int quantity);
    }
}
