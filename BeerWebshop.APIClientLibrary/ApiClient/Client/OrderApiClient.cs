using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class OrderApiClient : IOrderApiClient
    {
        private RestClient _restClient;
        public OrderApiClient(string uri) => _restClient = new RestClient(new Uri(uri));


        public async Task<OrderDTO> SaveOrder(OrderDTO OrderDTO)
        {   
            var response = await _restClient.RequestAsync<OrderDTO>(Method.Post, "Orders", OrderDTO);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating ProductDTO. Message was {response.Content}");
            }

            return response.Data;
        }
    }
}
