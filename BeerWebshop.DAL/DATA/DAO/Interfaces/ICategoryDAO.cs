using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface ICategoryDAO
{
	Task<int> CreateAsync(Category category);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<Category>> GetAllCategories();
	Task<int?> GetCategoryIdByName(string categoryName);
	Task<Category?> GetByIdAsync(int categoryId);
}
