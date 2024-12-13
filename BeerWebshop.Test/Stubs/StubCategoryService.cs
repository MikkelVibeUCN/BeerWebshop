using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;

namespace BeerWebshop.Test.Stubs;

public class StubCategoryService : ICategoryService
{
    public Task<int?> GetCategoryIdByName(string name) => Task.FromResult<int?>(1);
    public Task<Category?> GetCategoryById(int id) => Task.FromResult(new Category { Id = id, Name = "IPA" });

    public Task<IEnumerable<Category>> GetAlLCategories()
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}
