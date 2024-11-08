using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient
{
    public class OrderAPIClientStub : IOrderApiClient
    {
        public Task<OrderDTO> SaveOrder(OrderDTO OrderDTO)
        {
            OrderDTO.Id = 1;
            return Task.FromResult(OrderDTO);
        }
    }
}
