using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class OrderApiClient : BaseClient<OrderDTO>, IOrderApiClient
    {
        public OrderApiClient(string uri) : base(uri, "orders") { }

        public async Task<IEnumerable<OrderDTO>> GetLoggedInCustomerOrders(string jwtToken)
        {
            return await GetAllAsync($"{_defaultEndPoint}/LoggedInOrders", jwtToken);
        }

    }
}
