using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Test.APIClientLibraryTests;

[TestFixture]
public class ProductAPIClientIntegrationTests
{
	private ProductAPIClient _productApiClient;
	private int _createdProductId;

	[SetUp]
	public void SetUp()
	{
		string apiUri = "https://localhost:7244/api/v1/"; 
		_productApiClient = new ProductAPIClient(apiUri);
	}

	[Test]
	public async Task CreateProductAsync_WhenProductIsValid_ShouldReturnProductId()
	{
		var productDto = new ProductDTO
		{
			Name = "Integration Test Product",
			CategoryName = "IPA",
			BreweryName = "Overtone",
			Price = 10.0f,
			Description = "Sample product for integration test",
			Stock = 20,
			ABV = 5.5f,
			ImageUrl = "http://example.com/image.jpg",
			RowVersion = ""
		};

		_createdProductId = await _productApiClient.CreateAsync(productDto);

		Assert.That(_createdProductId, Is.GreaterThan(0), "Product creation should return a valid product ID.");
	}

	[Test]
	public async Task GetProductFromIdAsync_ShouldReturnProductDetails()
	{
		await CreateProductAsync_WhenProductIsValid_ShouldReturnProductId();

		var product = await _productApiClient.GetAsync(_createdProductId);

		Assert.NotNull(product);
		Assert.That(product.Name, Is.EqualTo("Integration Test Product"));
		Assert.That(product.CategoryName, Is.EqualTo("IPA"));
		Assert.That(product.BreweryName, Is.EqualTo("Overtone"));
	}

	[TearDown]
	public async Task TearDown()
	{
		if (_createdProductId > 0)
		{
			await _productApiClient.DeleteAsync(_createdProductId);
			_createdProductId = 0;
		}
	}
}
