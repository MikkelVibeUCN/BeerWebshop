using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DesktopClient.Controllers
{
    public class CategoryController
    {
        private ICategoryAPIClient _categoryAPIClient;
        public CategoryController(CategoryAPIClient categoryAPIClient) 
        {
            _categoryAPIClient = categoryAPIClient;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            return await _categoryAPIClient.GetAllAsync();
        }
    }
}
