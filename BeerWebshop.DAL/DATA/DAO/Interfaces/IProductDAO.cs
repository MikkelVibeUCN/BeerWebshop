using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IProductDAO
{
	Task<int> CreateAsync(Product Product);
	Task<Product?> GetByIdAsync(int id);
	Task<int> GetProductCountAsync(ProductQueryParameters parameters);
	Task<IEnumerable<string>> GetProductCategoriesAsync();
	Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters);
	Task EditAsync(int id);
	Task<bool> UpdateStockAsync(int productId, int quantity);
	Task<bool> DeleteAsync(int id);
}