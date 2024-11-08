using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public interface ICategoryAPIClient
{
	Task<int> CreateCategoryAsync(CategoryDTO category);
	Task<bool> DeleteAsync(int id);
}
