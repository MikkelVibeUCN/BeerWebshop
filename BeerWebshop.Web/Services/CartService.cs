using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using System.Linq;

namespace BeerWebshop.Web.Services
{
    public class CartService : ICartService
    {
        private readonly BeerService _beerService;
        private readonly CookieService _cookieService;
        private const string CartCookieKey = "Cart";

        public CartService(BeerService beerService, CookieService cookieService)
        {
            _beerService = beerService;
            _cookieService = cookieService;
        }

        public ShoppingCart GetCart()
        {
            return GetCartFromCookies();
        }

        private bool HasProductInCart(int productId)
        {
            var orderLine = GetCart().OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            return orderLine != null;
        }

        public async void AddToCart(int productId, int quantity)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }

            Product? beer = await _beerService.GetProductFromId(productId);
            if (beer == null)
            {
                throw new Exception("Beer not found");
            }

            if (!HasEnoughStock(beer, quantity))
            {
                throw new Exception("Not enough stock");
            }

            var cart = GetCart();

            if (HasProductInCart(beer.Id))
            {
                OrderLine orderLine = cart.OrderLines.First(ol => ol.Product.Id == beer.Id);
                UpdateQuantity(beer.Id, orderLine.Quantity + quantity, cart);
            }
            else
            {
                var orderLine = new OrderLine(quantity, beer);
                cart.AddOrderLine(orderLine);
            }

            SaveCartToCookies(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();

            if (!HasProductInCart(productId))
            {
                throw new Exception("Product not found in cart");
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
                throw new Exception("Product not found in cart");
            }

            var orderLineToUpdate = cart.OrderLines.First(ol => ol.Product.Id == productId);

            if (!HasEnoughStock(orderLineToUpdate.Product, newQuantity))
            {
                throw new Exception("Not enough stock");
            }
            orderLineToUpdate.Quantity = newQuantity;

            SaveCartToCookies(cart);
        }

        public bool HasEnoughStock(Product product, int quantity)
        {
            return product.Stock >= quantity;
        }

        public Product GetProductFromOrderlines(int productId)
        {
            var orderLine = GetCart().OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            if (orderLine == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found in order lines.");
            }
            return orderLine.Product;
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
    }
}
