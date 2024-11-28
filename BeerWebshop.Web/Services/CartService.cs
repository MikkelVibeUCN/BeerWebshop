using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;
using System.Linq;

namespace BeerWebshop.Web.Services
{
    public class CartService : ICartService
    {
        private readonly CookieService _cookieService;
        private const string CartCookieKey = "Cart";

        public CartService(CookieService cookieService)
        {
            _cookieService = cookieService;
        }
        public ShoppingCart GetCart()
        {
            return GetCartFromCookies();
        }

        private bool HasProductInCart(int productId)
        {
            var orderLineDTO = GetCart().OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            return orderLineDTO != null;
        }

        public void AddToCart(ProductDTO product, int quantity)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }

            if (!HasEnoughStock(product, quantity))
            {
                throw new Exception("Not enough stock");
            }

            var cart = GetCart();

            if (HasProductInCart(product.Id))
            {
                OrderLineDTO orderLineDTO = cart.OrderLines.First(ol => ol.Product.Id == product.Id);
                UpdateQuantity(product.Id, orderLineDTO.Quantity + quantity, cart);
            }
            else
            {
                var orderLineDTO = new OrderLineDTO(quantity, product);
                cart.AddOrderLine(orderLineDTO);
            }

            SaveCartToCookies(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();

            if (!HasProductInCart(productId))
            {
                throw new Exception("ProductDTO not found in cart");
            }

            var orderLineToRemove = cart.OrderLines.First(ol => ol.Product.Id == productId);
            cart.OrderLines.Remove(orderLineToRemove);

            SaveCartToCookies(cart);
        }

        public void UpdateQuantity(int productId, int newQuantity, ShoppingCart? cart = null)
        {
            if(cart == null)
            {
                cart = GetCart();
            }

            if (!HasProductInCart(productId))
            {
                throw new Exception("ProductDTO not found in cart");
            }

            var orderLineToUpdate = cart.OrderLines.First(ol => ol.Product.Id == productId);

            if (!HasEnoughStock(orderLineToUpdate.Product, newQuantity))
            {
                throw new Exception("Not enough stock");
            }
            orderLineToUpdate.Quantity = newQuantity;

            SaveCartToCookies(cart);
        }

        public bool HasEnoughStock(ProductDTO ProductDTO, int quantity)
        {
            return ProductDTO.Stock >= quantity;
        }

        public ProductDTO GetProductFromOrderlines(int productId)
        {
            var orderLineDTO = GetCart().OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            if (orderLineDTO == null)
            {
                throw new InvalidOperationException($"ProductDTO with ID {productId} not found in OrderDTO lines.");
            }
            return orderLineDTO.Product;
        }
        public ShoppingCart GetCartFromCookies()
        {
            ShoppingCart? cart = _cookieService.GetObjectFromCookie<ShoppingCart>(CartCookieKey);

            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            return cart;
        }
        public void SaveCartToCookies(ShoppingCart cart)
        {
            _cookieService.SaveCookie<ShoppingCart>(cart, CartCookieKey);
        }

        public void ClearCartCookies()
        {
            _cookieService.RemoveCookies<ShoppingCart>(CartCookieKey);
        }
    }
}
