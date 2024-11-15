﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public abstract class BaseClient<T>
    {
        protected RestClient _restClient;
        protected string _defaultEndPoint;
        public BaseClient(string uri, string defaultEndpoint) 
        {
            _restClient = new RestClient(new Uri(uri));
            _defaultEndPoint = defaultEndpoint;
        }
        public async Task<int> CreateAsync(T entity, string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var response = await _restClient.RequestAsync<int>(Method.Post, endpoint, entity);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> DeleteAsync(int id, string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var response = await _restClient.RequestAsync<bool>(Method.Delete, $"{endpoint}/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error deleting {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var response = await _restClient.RequestAsync<IEnumerable<T>>(Method.Get, endpoint);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error retrieving all {typeof(T).Name}. Message was {response.Content}");
            }

            return response.Data ?? Enumerable.Empty<T>();
        }

        public async Task<T?> GetAsync(int id, string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var response = await _restClient.RequestAsync<T>(Method.Get, $"{endpoint}/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<T?> GetByString(string searchKey, string? endpoint = null)
        {
            endpoint ??= _defaultEndPoint;
            var response = await _restClient.RequestAsync<T>(Method.Get, endpoint);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving {typeof(T).Name}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<U>> GetAllAsync<U>(string endpoint) 
        {
            return await GetAllAsync<U>(endpoint);
        }
    }
}
