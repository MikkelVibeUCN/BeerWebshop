using BeerWebshop.Web.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class CartServiceStop : ICartService
    {
        public ShoppingCart Cart { get; set; }
        private readonly BeerService _beerService;

        public CartServiceStop(BeerService beerService)
        {
            _beerService = beerService;
            Cart = GetCart();
        }

        private ShoppingCart CreateShoppingCart()
        {
            var product1 = new Product
            {
                Id = 1,
                Name = "Lager",
                Brewery = "Brewery A",
                Price = 3.5f,
                Description = "A smooth, refreshing lager.",
                Stock = 50,
                ABV = 4.5f,
                Type = "Lager",
                Url = "http://example.com/lager"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "IPA",
                Brewery = "Brewery B",
                Price = 5.0f,
                Description = "A hoppy IPA with citrus notes.",
                Stock = 30,
                ABV = 6.5f,
                Type = "IPA",
                Url = "http://example.com/ipa"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Stout",
                Brewery = "Brewery C",
                Price = 4.0f,
                Description = "A rich, dark stout.",
                Stock = 20,
                ABV = 8.0f,
                Type = "Stout",
                Url = "http://example.com/stout"
            };

            var product4 = new Product
            {
                Id = 4,
                Name = "Pilsner",
                Brewery = "Brewery D",
                Price = 3.0f,
                Description = "A classic pilsner.",
                Stock = 25,
                ABV = 4.0f,
                Type = "Pilsner",
                Url = "http://example.com/pilsner"
            };

            var product5 = new Product
            {
                Id = 5,
                Name = "Porter",
                Brewery = "Brewery E",
                Price = 4.5f,
                Description = "A smooth, creamy porter.",
                Stock = 15,
                ABV = 7.0f,
                Type = "Porter",
                Url = "http://example.com/porter"
            };

            // Creating order lines
            var orderLine1 = new OrderLine(2, product1); // 2 Lagers
            var orderLine2 = new OrderLine(1, product2); // 1 IPA
            var orderLine3 = new OrderLine(3, product3); // 3 Stouts
            var orderLine4 = new OrderLine(1, product4); // 1 Pilsner
            var orderLine5 = new OrderLine(2, product5); // 2 Porters

            // Create a shopping cart and add the order lines
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddOrderLine(orderLine1);
            shoppingCart.AddOrderLine(orderLine2);
            shoppingCart.AddOrderLine(orderLine3);
            shoppingCart.AddOrderLine(orderLine4);
            shoppingCart.AddOrderLine(orderLine5);

            Cart = shoppingCart;

            return Cart;
        }

        public ShoppingCart GetCart()
        {
            if(Cart == null)
            {
                Cart = CreateShoppingCart();
            }
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
            if(quantity == 0)
            {
                quantity = 1;
            }

            Product beer = _beerService.GetBeerFromId(productId);

            if(HasProductInCart(beer.Id))
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
            if(HasProductInCart(productId))
            {
                var orderLineToRemove = Cart.OrderLines.First(ol => ol.Product.Id == productId);
                Cart.OrderLines.Remove(orderLineToRemove);
            }
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            if(HasProductInCart(productId))
            {
                var orderLineToUpdate = Cart.OrderLines.First(ol => ol.Product.Id == productId);
                orderLineToUpdate.Quantity = newQuantity;
            }
        }
    }
}