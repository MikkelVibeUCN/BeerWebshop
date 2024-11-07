using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeerWebshop.APIClientLibrary.ApiClient.Client
{
    public interface IProductAPIClient
    {
        Task<Product?> GetProductFromIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<string>> GetProductCategoriesAsync();

        Task<int> CreateProductAsync(Product Product);

    }
}
