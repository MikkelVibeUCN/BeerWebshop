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

        public IEnumerable<Product> GetTenLatestBeers()
        {
            return _restClient.GetTenLatestBeers();
        }
        public Product GetBeerFromId(int id)
        {
            return _restClient.GetBeerFromId(id);
        }
    }
}
