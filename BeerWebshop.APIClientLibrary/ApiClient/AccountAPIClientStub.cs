using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient
{
    public class AccountAPIClientStub : ICustomerAPIClient
    {
        public Task<int> CreateCustomer(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO?> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
