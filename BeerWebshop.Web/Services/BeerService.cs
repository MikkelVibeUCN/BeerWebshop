using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
    public class BeerService
    {
        private readonly IProductAPIClient _productAPIClient;
        private readonly ICategoryAPIClient _categoryAPIClient;

        public BeerService(IProductAPIClient restClient, ICategoryAPIClient categoryAPIClient)
        {
            _productAPIClient = restClient;
            _categoryAPIClient = categoryAPIClient;
        }

        public async Task<int> GetProductCount(ProductQueryParameters parameters)
        {
            return await _productAPIClient.GetProductCountAsync(parameters);
        }

        public async Task<ProductDTO?> GetProductFromId(int id)
        {
            return await _productAPIClient.GetProductFromIdAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts(ProductQueryParameters parameters)
        {
            return await _productAPIClient.GetProductsAsync(parameters);
        }

        public async Task<IEnumerable<string>> GetProductCategories()
        {
            IEnumerable<CategoryDTO?> categories = await _categoryAPIClient.GetAllCategories();
            return categories.Select(c => c?.Name ?? string.Empty);
        }
    }
}
