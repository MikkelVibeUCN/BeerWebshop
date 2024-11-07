using BeerWebshop.APIClientLibrary;
using BeerWebshop.Web.ApiClient;
using BeerWebshop.Web.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class BeerService
    {
        private readonly IRestClient _restClient;

        public BeerService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Product?> GetBeerFromId(int id)
        {
            return await _restClient.GetBeerFromId(id);
        }

        public async Task<List<Product>> GetBeers(ProductQueryParameters parameters)
        {
            return await _restClient.GetBeers(parameters);
        }

        public async Task<List<string>> GetBeerCategories()
        {
            return await _restClient.GetBeerCategories();
        }
    }
}
