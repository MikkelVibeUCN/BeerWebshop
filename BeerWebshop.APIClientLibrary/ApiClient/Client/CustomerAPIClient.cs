using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class CustomerAPIClient : ICustomerAPIClient
    {
        private readonly RestClient _restClient;
        public CustomerAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));
        
        public async Task<int> CreateCustomer(CustomerDTO customer)
        {
            var response = await _restClient.RequestAsync<int>(Method.Post, "customers", customer);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating Customer. Message was {response.Content}");
            }

            return response.Data;
        }

        public Task<CustomerDTO?> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
