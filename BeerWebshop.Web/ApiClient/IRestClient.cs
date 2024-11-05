using BeerWebshop.Web.ApiClient.DTO;

namespace BeerWebshop.Web.ApiClient
{
    public interface IRestClient
    {
        IEnumerable<Product> GetTenLatestBeers();
        Product GetBeerFromId(int id);
        int AddNewBeer(Product product);


    }
}
