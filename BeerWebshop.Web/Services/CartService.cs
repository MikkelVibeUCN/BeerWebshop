using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;
using BeerWebshop.Web.Models;
using System.Linq;

namespace BeerWebshop.Web.Services
{
    public class CartService : ICartService
    {
        private readonly CookieService _cookieService;
        private readonly ProductService _productService;
        private const string CartCookieKey = "Cart";

        public CartService(CookieService cookieService, ProductService productService)
        {
            _cookieService = cookieService;
            productService = _productService;
        }
        public async Task<ShoppingCart> GetCartViewModel()
        {
            CartCookie cartCookie = GetCartFromCookies();
            return await ConvertCookieToCart(cartCookie);
        }

        private OrderLineCutDown? GetOrderLineInCart(int productId)
        {
            return GetCartFromCookies().OrderLines.FirstOrDefault(ol => ol.ProductId == productId);
        }

        public void AddToCart(ProductDTO product, int quantity)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }

            if (!HasEnoughStock(product.Stock, quantity))
            {
                throw new Exception("Not enough stock");
            }

            var cart = GetCartFromCookies();

            OrderLineCutDown? orderLine = GetOrderLineInCart(product.Id);
            if(orderLine == null)
            {
                orderLine = new OrderLineCutDown
                {
                    ProductId = product.Id,
                    Quantity = quantity
                };
                cart.OrderLines.Add(orderLine);
            }
            else
            {
                orderLine.Quantity += quantity;
            }
            
            SaveCartToCookies(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartFromCookies();

            OrderLineCutDown? orderLineToRemove = GetOrderLineInCart(productId);
            if (orderLineToRemove == null)
            {
                throw new Exception("Product not found in cart");
            }
            cart.OrderLines.Remove(orderLineToRemove);

            SaveCartToCookies(cart);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        private bool HasEnoughStock(int productStock, int quantity) => productStock >= quantity;

        private CartCookie GetCartFromCookies()
        {
            CartCookie? cart = _cookieService.GetObjectFromCookie<CartCookie?>(CartCookieKey);

            if (cart == null)
            {
                cart = new CartCookie();
            }
            return cart;
        }
        public void SaveCartToCookies(ShoppingCart cart)
        {
            SaveCartToCookies(cart.ConvertToCookie());
        }

        public void SaveCartToCookies(CartCookie cart)
        {
            _cookieService.SaveCookie<CartCookie>(cart, CartCookieKey);
        }

        public void ClearCartCookies()
        {
            _cookieService.RemoveCookies<CartCookie>(CartCookieKey);
        }

        private async Task<ShoppingCart> ConvertCookieToCart(CartCookie cartCookie)
        {
            var shoppingCart = new ShoppingCart();

            foreach (var orderLineCutDown in cartCookie.OrderLines)
            {
                var product = await _productService.GetProductFromId(orderLineCutDown.ProductId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                var orderLine = new OrderLineDTO
                {
                    Quantity = orderLineCutDown.Quantity,
                    Product = new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        BreweryName = product.BreweryName,
                        Price = product.Price,
                        Description = product.Description,
                        Stock = product.Stock,
                        ABV = product.ABV,
                        CategoryName = product.BreweryName,
                        ImageUrl = product.ImageUrl,
                    }
                };

                shoppingCart.OrderLines.Add(orderLine);
            }

            return shoppingCart;
        }

        
    }
}
