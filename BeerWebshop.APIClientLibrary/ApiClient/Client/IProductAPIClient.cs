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
        Task<ProductDTO?> GetProductFromIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<string>> GetProductCategoriesAsync();

    }
}
