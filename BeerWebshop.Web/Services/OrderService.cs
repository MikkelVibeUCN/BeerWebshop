using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class OrderService
    {
        private readonly IOrderApiClient _orderAPIClient;
        public OrderService(IOrderApiClient orderAPIClient)
        {
            _orderAPIClient = orderAPIClient;
        }

        public async Task<OrderDTO> SaveOrder(CheckoutViewModel model)
        {

            CustomerDTO customerDTO = CreateCustomerFromCheckout(model.Checkout);

            OrderDTO orderDTO = new OrderDTO
            {
                CustomerDTO = customerDTO,
                OrderLines = model.Cart.OrderLines,
                Date = DateTime.Now,
            };

            return await _orderAPIClient.SaveOrder(orderDTO);
        }

        private CustomerDTO CreateCustomerFromCheckout(Checkout checkout)
        {
            string name = checkout.Firstname + " " + checkout.Lastname;

            string address = checkout.Street + " " + checkout.Number + ", " + checkout.PostalCode + " " + checkout.City;

            return new CustomerDTO
            {
                Name = name,
                Address = address,
                Phone = checkout.Phonenumber
            };
        }

    }
}
