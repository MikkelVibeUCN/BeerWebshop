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
        Task<Product?> GetProductFromId(int id);
        Task<List<Product>> GetProducts(ProductQueryParameters parameters);
        Task<List<string>> GetProductCategories();

    }
}
