using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IProductDAO : IBaseDAO<Product>
{

	Task<int> GetProductCountAsync(ProductQueryParameters parameters);
	Task<IEnumerable<string>> GetProductCategoriesAsync();
	Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters);
	Task<bool> UpdateStockAsync(int productId, int quantity);
}