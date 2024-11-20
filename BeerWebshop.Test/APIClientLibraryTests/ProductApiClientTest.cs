using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWebshop.Test.APIClientLibraryTests
{
	[TestFixture]
	[NonParallelizable]
	public class ProductAPIClientTest
	{
		private ProductAPIClient _productApiClient;
		private CategoryAPIClient _categoryApiClient;
		private BreweryAPIClient _breweryApiClient;

		private readonly string _testSuffix = $"_Test";
		private readonly List<int> _createdProductIds = new();
		private readonly List<int> _createdCategoryIds = new();
		private readonly List<int> _createdBreweryIds = new();

		[OneTimeSetUp]
		public async Task SetUpAsync()
		{
			string apiUri = "https://localhost:7244/api/v1/";
			_productApiClient = new ProductAPIClient(apiUri);
			_categoryApiClient = new CategoryAPIClient(apiUri);
			_breweryApiClient = new BreweryAPIClient(apiUri);

			var categoryDto = new CategoryDTO { Name = $"IPA{_testSuffix}" };
			_createdCategoryIds.Add(await _categoryApiClient.CreateAsync(categoryDto));

			var breweryDto = new BreweryDTO { Name = $"Overtone{_testSuffix}" };
			_createdBreweryIds.Add(await _breweryApiClient.CreateAsync(breweryDto));
		}

		[OneTimeTearDown]
		public async Task TearDownAsync()
		{
			// Delete products
			foreach (var productId in _createdProductIds)
			{
				try
				{
					await _productApiClient.DeleteAsync(productId);
				}
				catch (Exception ex)
				{
					TestContext.WriteLine($"Failed to delete product with ID {productId}: {ex.Message}");
				}
			}

			// Delete breweries
			foreach (var breweryId in _createdBreweryIds)
			{
				try
				{
					await _breweryApiClient.DeleteAsync(breweryId);
				}
				catch (Exception ex)
				{
					TestContext.WriteLine($"Failed to delete brewery with ID {breweryId}: {ex.Message}");
				}
			}

			// Delete categories
			foreach (var categoryId in _createdCategoryIds)
			{
				try
				{
					await _categoryApiClient.DeleteAsync(categoryId);
				}
				catch (Exception ex)
				{
					TestContext.WriteLine($"Failed to delete category with ID {categoryId}: {ex.Message}");
				}
			}

			_createdProductIds.Clear();
			_createdCategoryIds.Clear();
			_createdBreweryIds.Clear();
		}

		[Test]
		public async Task CreateProductAsync_ShouldReturnProductId()
		{
			var productDTO = CreateTestProductDto();
			var productId = await _productApiClient.CreateAsync(productDTO);
			productDTO.Id = productId;
			_createdProductIds.Add(productId);
			productDTO.Name = productDTO.Name + "_test1";

			Assert.IsTrue(productId > 0);
		}

		[Test]
		public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
		{
			var productDTO = CreateTestProductDto();
			var productId = await _productApiClient.CreateAsync(productDTO);
			_createdProductIds.Add(productId);
			var product = await _productApiClient.GetAsync(productId);

			Assert.IsNotNull(product);
			Assert.AreEqual(productDTO.Name, product.Name);
			Assert.AreEqual(productDTO.CategoryName, product.CategoryName);
			Assert.AreEqual(productDTO.BreweryName, product.BreweryName);
			Assert.AreEqual(productDTO.Price, product.Price);
			Assert.AreEqual(productDTO.Description, product.Description);
			Assert.AreEqual(productDTO.Stock, product.Stock);
			Assert.AreEqual(productDTO.ABV, product.ABV);
		}

		[Test]
		public async Task DeleteProductAsync_ShouldRemoveProduct()
		{
			var productDTO = CreateTestProductDto();
			var productId = await _productApiClient.CreateAsync(productDTO);
			_createdProductIds.Add(productId);
			productDTO.Id = productId;

			await _productApiClient.DeleteAsync(productId);

			Assert.ThrowsAsync<Exception>(async () => await _productApiClient.DeleteAsync(productId));
		}

		private ProductDTO CreateTestProductDto()
		{
			return new ProductDTO
			{
				Id = 0,
				Name = $"Integration Test Product{_testSuffix}",
				CategoryName = $"IPA{_testSuffix}",
				BreweryName = $"Overtone{_testSuffix}",
				Price = 10.0f,
				Description = $"Sample product for integration test{_testSuffix}",
				Stock = 20,
				ABV = 5.5f,
				ImageUrl = "http://example.com/image.jpg",
				RowVersion = ""
			};
		}
	}
}
