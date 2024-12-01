using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public interface ICartService
    {

        Task<ShoppingCart> GetCartViewModel();
        void RemoveFromCart(int productId);

        void UpdateQuantity(int productId, int quantity);

        void AddToCart(ProductDTO product, int quantity);
            
    }
}
