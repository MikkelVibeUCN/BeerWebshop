using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services
{
	public class CategoryService
	{
		private readonly ICategoryDAO _categoryDAO;
		public CategoryService(ICategoryDAO categoryDAO)
		{
			_categoryDAO = categoryDAO;
		}

		public async Task<IEnumerable<Category>> GetAlLCategories()
		{
			return await _categoryDAO.GetAllCategories();
		}

		public async Task<int> CreateCategoryAsync(Category category)
		{
			return await _categoryDAO.CreateCategoryAsync(category);
		}

		public async Task<int?> GetCategoryIdByName(string name)
		{
			return await _categoryDAO.GetCategoryIdByName(name);
		}

		public async Task<Category?> GetCategoryById(int id)
		{
			return await _categoryDAO.GetCategoryByIdAsync(id);
		}



	}
}
