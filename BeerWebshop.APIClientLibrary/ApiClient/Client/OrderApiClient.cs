using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
	public class OrderApiClient : IOrderApiClient
	{
		private RestClient _restClient;
		public OrderApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

		public async Task<OrderDTO?> GetOrderFromId(int id)
		{
			var response = await _restClient.RequestAsync<OrderDTO>(Method.Get, $"Orders/{id}");

			if (!response.IsSuccessful)
			{
				throw new Exception("Error getting order");
			}
			return response.Data;
		}

		public async Task<int> SaveOrder(OrderDTO orderDTO)
		{
			var response = await _restClient.RequestAsync<int>(Method.Post, "Orders/", orderDTO);

			if (!response.IsSuccessful)
			{
				throw new Exception($"Error creating Order. Message was {response.Content}");
			}

			return response.Data;
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

		public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
		{
			throw new NotImplementedException();
		}
	}
}
