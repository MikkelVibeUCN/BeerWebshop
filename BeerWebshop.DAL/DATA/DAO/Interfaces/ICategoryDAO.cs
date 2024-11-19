using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface ICategoryDAO : IBaseDAO<Category>
{
	Task<int?> GetCategoryIdByName(string categoryName);
	
}
