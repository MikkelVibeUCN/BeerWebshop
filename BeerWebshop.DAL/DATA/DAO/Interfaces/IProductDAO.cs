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
    Task<Product> GetByIdAsync(int id);

    Task<IEnumerable<Product>> GetFromCategoryAsync(string category);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<string>> GetProductCategoriesAsync();

    Task<int?> GetCategoryIdByName(string categoryName);
    Task<int?> GetBreweryIdByName(string breweryName);

}
