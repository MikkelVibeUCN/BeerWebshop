using BeerWebshop.APIClientLibrary.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.Client
{
    public class ProductAPIClient : IProductAPIClient
    {
        private RestClient _restClient;
        public ProductAPIClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<BeerDTO> GetBeerByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<BeerDTO>(Method.Get, $"Beers/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all beers. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
        public Task<IEnumerable<BeerDTO>> GetAllBeersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> CreateBeerAsync(BeerDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateBeerAsync(BeerDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBeerAsync(int id)
        {
            throw new NotImplementedException();
        }



    }
}
