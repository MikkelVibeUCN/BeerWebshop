using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services.Interfaces;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetAlLCategories();

    public Task<int> CreateCategoryAsync(Category category);

    public Task<int?> GetCategoryIdByName(string name);

    public Task<Category?> GetCategoryById(int id);

    public Task<bool> DeleteCategoryAsync(int id);
}
