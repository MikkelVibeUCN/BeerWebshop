using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;

namespace BeerWebshop.RESTAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDAO _categoryDAO;
        public CategoryService(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public async Task<IEnumerable<Category>> GetAlLCategories()
        {
            return await _categoryDAO.GetAllAsync();
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            return await _categoryDAO.CreateAsync(category);
        }

        public async Task<int?> GetCategoryIdByName(string name)
        {
            return await _categoryDAO.GetCategoryIdByName(name);
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _categoryDAO.GetByIdAsync(id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryDAO.DeleteAsync(id);
        }



    }
}
