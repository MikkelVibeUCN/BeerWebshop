using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class BeerService
    {
        private readonly IProductAPIClient _productAPIClient;

        public BeerService(IProductAPIClient restClient)
        {
            _productAPIClient = restClient;
        }

        public async Task<Product?> GetProductFromId(int id)
        {
            return await _productAPIClient.GetProductFromIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
        {
            return await _productAPIClient.GetProductsAsync(parameters);
        }

        public async Task<IEnumerable<string>> GetProductCategories()
        {
            return await _productAPIClient.GetProductCategoriesAsync();
        }
    }
}
