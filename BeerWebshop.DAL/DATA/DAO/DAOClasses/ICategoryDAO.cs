using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public interface ICategoryDAO
{
	Task<int> CreateCategoryAsync(Category category);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<Category>> GetAllCategories();

	Task<int?> GetCategoryIdByName(string categoryName);
	Task<Category?> GetCategoryByIdAsync(int categoryId);
	Task<Category?> GetCategoryById(int id);
}
