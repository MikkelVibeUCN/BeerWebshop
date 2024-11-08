using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public interface ICartService
    {

        ShoppingCart GetCart();
        void RemoveFromCart(int productId);

        void UpdateQuantity(int productId, int quantity, ShoppingCart? cart = null);

        void AddToCart(int productId, int quantity);

        bool HasEnoughStock(ProductDTO productDTO, int quantity);

        ProductDTO GetProductFromOrderlines(int productId);
        void ClearCartCookies();
    }
}
