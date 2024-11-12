using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface ICategoryAPIClient
{
	Task<int> CreateCategoryAsync(CategoryDTO category);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<CategoryDTO?>> GetAllCategories();
}
