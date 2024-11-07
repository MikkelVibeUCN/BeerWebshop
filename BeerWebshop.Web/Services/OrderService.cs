using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class OrderService
    {
        private readonly IOrderAPIClient _orderAPIClient;
        public OrderService(IOrderAPIClient orderAPIClient)
        {
            _orderAPIClient = orderAPIClient;
        }


        public async Task<Order> SaveOrder(CheckoutViewModel model)
        {

            Customer customer = CreateCustomerFromCheckout(model.Checkout);

            Order order = new Order
            {
                Customer = customer,
                OrderLines = model.Cart.OrderLines,
                Date = DateTime.Now,
            };

            return await _orderAPIClient.SaveOrder(order);
        }

        private Customer CreateCustomerFromCheckout(Checkout checkout)
        {
            string name = checkout.Firstname + " " + checkout.Lastname;

            string address = checkout.Street + " " + checkout.Number + ", " + checkout.PostalCode + " " + checkout.City;

            return new Customer
            {
                Name = name,
                Address = address,
                Phone = checkout.Phonenumber
            };
        }

    }
}
