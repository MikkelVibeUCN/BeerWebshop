using BeerWebshop.Web.ApiClient.DTO;

namespace BeerWebshop.Web.ApiClient
{
    public interface IRestClient
    {
        IEnumerable<Beer> GetTenLatestBeers();
        Beer GetBeerFromId(int id);
        int AddNewBeer(Beer beer);

    }
}
