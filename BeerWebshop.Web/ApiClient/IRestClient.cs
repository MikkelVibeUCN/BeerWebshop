using BeerWebshop.APIClientLibrary;
using BeerWebshop.Web.ApiClient.DTO;

namespace BeerWebshop.Web.ApiClient
{
    public interface IRestClient
    {
        Task<Product?> GetBeerFromId(int id);
        Task<List<Product>> GetBeers(ProductQueryParameters parameters);
        Task<List<string>> GetBeerCategories();

    }
}
