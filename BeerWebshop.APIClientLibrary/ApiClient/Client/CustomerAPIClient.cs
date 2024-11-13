using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class CustomerAPIClient : BaseClient<CustomerDTO>, IAccountAPIClient
    {
        public CustomerAPIClient(string uri) : base(uri, "Accounts") { }
    }
}
