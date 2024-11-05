using BeerWebshop.Web.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class CartService : ICartService
    {
        private readonly BeerService _beerService;
        public ShoppingCart Cart { get; set; }
        public CartService(BeerService beerService)
        {
            Cart = new ShoppingCart();
            _beerService = beerService;
        }
        public ShoppingCart GetCart()
        {
            return Cart;
        }

        private bool HasProductInCart(int productId)
        {
            var orderLine = Cart.OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            if (orderLine != null)
            {
                return true;
            }
            return false;
        }

        public void AddToCart(int productId, int quantity)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }

            Product beer = _beerService.GetBeerFromId(productId);

            if (!HasEnoughStock(beer, quantity))
            {
                throw new Exception("Not enough stock");
            }

            if (HasProductInCart(beer.Id))
            {
                OrderLine orderLine = Cart.OrderLines.First(ol => ol.Product.Id == beer.Id);

                UpdateQuantity(beer.Id, orderLine.Quantity + quantity);
            }
            else
            {
                var orderLine = new OrderLine(quantity, beer);
                Cart.AddOrderLine(orderLine);
            }
        }

        public void RemoveFromCart(int productId)
        {
            if (!HasProductInCart(productId))
            {
                throw new Exception("Product not found in cart");
            }

            var orderLineToRemove = Cart.OrderLines.First(ol => ol.Product.Id == productId);
            Cart.OrderLines.Remove(orderLineToRemove);
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            if (!HasProductInCart(productId))
            {
                throw new Exception("Product not found in cart");
            }

            var orderLineToUpdate = Cart.OrderLines.First(ol => ol.Product.Id == productId);

            if (!HasEnoughStock(orderLineToUpdate.Product, newQuantity))
            {
                throw new Exception("Not enough stock");
            }

            orderLineToUpdate.Quantity = newQuantity;
        }

        public bool HasEnoughStock(Product product, int quantity)
        {
            return product.Stock >= quantity;
        }

        public Product GetProductFromOrderlines(int productId)
        {
            var orderLine = Cart.OrderLines.FirstOrDefault(ol => ol.Product.Id == productId);
            if (orderLine == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found in order lines.");
            }
            return orderLine.Product;
        }
    }
}
