using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
	public class OrderApiClient : IOrderApiClient
	{
		private RestClient _restClient;
		public OrderApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

		public Task<OrderDTO?> GetOrderFromId(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<int> SaveOrder(OrderDTO Order)
		{
			var response = await _restClient.RequestAsync<OrderDTO>(Method.Post, "Orders", Order);

			if (!response.IsSuccessful)
			{
				throw new Exception($"Error creating Order. Message was {response.Content}");
			}

			return response.Data.Id;
		}

		public async Task<bool> DeleteOrder(int id)
		{
			var response = await _restClient.RequestAsync(Method.Delete, $"Orders/{id}");

			if (!response.IsSuccessful)
			{
				throw new Exception($"Error deleting OrderDTO. Message was {response.Content}");
			}

			return true;
		}
	}
}
