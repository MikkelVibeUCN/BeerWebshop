using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public abstract class BaseClient<T> where T : class
    {
        protected RestClient _restClient;
        protected string _defaultEndPoint;

        public BaseClient(string baseUri, string defaultEndpoint)
        {
            _restClient = new RestClient(baseUri);
            _defaultEndPoint = defaultEndpoint;
        }

        public async Task<int> CreateAsync(T entity, string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(entity);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<int>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> DeleteAsync(int id, string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest($"{endpoint}/{id}", Method.Delete);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<bool>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error deleting {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest(endpoint, Method.Get);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<IEnumerable<T>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error retrieving all {typeof(T).Name}. Message was {response.Content}");
            }

            return response.Data;
        }

        public async Task<T?> GetAsync(int id, string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest($"{endpoint}/{id}", Method.Get);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<T>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<T?> GetByStringAsync(string searchKey, string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest(endpoint, Method.Get);
            request.AddParameter("searchKey", searchKey);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<T>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<W> GetByStringAsync<W>(string searchKey, string? endpoint = null, string? jwtToken = null)
        {
            endpoint ??= _defaultEndPoint;

            var request = new RestRequest(endpoint, Method.Get);
            request.AddParameter("searchKey", searchKey);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<W>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving {typeof(W).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<U>> GetAllAsync<U>(string endpoint, string? jwtToken = null)
        {
            var request = new RestRequest(endpoint, Method.Get);
            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }

            var response = await _restClient.ExecuteAsync<IEnumerable<U>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error retrieving {typeof(U).Name}. Message was {response.Content}");
            }

            return response.Data;
        }
    }
}
