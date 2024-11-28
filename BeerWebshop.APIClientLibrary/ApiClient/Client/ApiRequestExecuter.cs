using RestSharp;
using System.Text.Json;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
	public static class ApiRequestExecuter
	{
        public static async Task<RestResponse<T>> RequestAsync<T>(this RestClient client, Method method, string resource, object body = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException("Resource cannot be null or empty.", nameof(resource));
            }

            var request = new RestRequest(resource, method);

            if (body != null)
            {
                request.AddJsonBody(body); 
            }

            var response = await client.ExecuteAsync<T>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Request failed with status {response.StatusCode}: {response.Content}");
            }

            return response;
        }


        public static async Task<RestResponse> RequestAsync(this RestClient client, Method method, string resource, object body = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException("Resource cannot be null or empty.", nameof(resource));
            }

            var request = new RestRequest(resource, method);

            if (body != null)
            {
                request.AddJsonBody(body); 
            }

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Request failed with status {response.StatusCode}: {response.Content}");
            }
            return response;
        }
    }
}
