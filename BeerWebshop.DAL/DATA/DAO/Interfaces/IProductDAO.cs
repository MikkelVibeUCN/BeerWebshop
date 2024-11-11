using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IProductDAO
{
    Task<int> CreateAsync(Product Product);
    Task<Product?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task EditAsync(int id);

    Task<IEnumerable<string>> GetProductCategoriesAsync();
    Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters);
    Task<int> GetProductCountAsync(ProductQueryParameters parameters);

    Task<bool> UpdateStockOptimisticAsync(int productId, int quantity, byte[] rowVersion);
}