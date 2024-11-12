using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.APIClientLibraryTests
{
    public class CategoryAPIClientTest
    {
        private readonly ICategoryAPIClient _categoryAPIClient;

        public CategoryAPIClientTest()
        {
            _categoryAPIClient = new CategoryAPIClient("https://localhost:7244/api/v1/");
        }

        [Test]
        public async Task GetProductCategories_WhenCategoriesExist_ShouldReturnAllCategories()
        {
            var categories = (await _categoryAPIClient.GetAllCategories()).ToList();
            Assert.That(categories != null);
            Assert.That(categories.Any(c => c.Name == "IPA"));
            Assert.That(!categories.Any(c => c.Name == "ThomasNumseJuice"));
        }
    }
}
