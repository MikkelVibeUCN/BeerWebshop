using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient
{
    public class OrderAPIClientStub : IOrderAPIClient
    {
        public Task<Order> SaveOrder(Order order)
        {
            order.Id = 1;
            return Task.FromResult(order);
        }
    }
}
