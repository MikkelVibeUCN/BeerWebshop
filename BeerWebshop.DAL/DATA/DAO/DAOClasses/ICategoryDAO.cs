using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public interface ICategoryDAO
{
	Task<int> CreateCategoryAsync(Category category);
	Task<bool> DeleteAsync(int id);
}
