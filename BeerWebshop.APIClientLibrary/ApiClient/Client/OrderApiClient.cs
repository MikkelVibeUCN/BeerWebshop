using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public class OrderApiClient : BaseClient<OrderDTO>, IOrderApiClient
    {
        public OrderApiClient(string uri) : base(uri, "Orders") { }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId, string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var completeEndpoint = $"{endpoint}/{customerId}/orders";

            return await GetByStringAsync<IEnumerable<OrderDTO>>(string.Empty, completeEndpoint);
        }

    }
}
