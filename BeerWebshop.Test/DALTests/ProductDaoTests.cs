using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.Extensions.Configuration;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class ProductDaoTests
{
	private ProductDAO _productDao;
	private CategoryDAO _categoryDao;
	private BreweryDAO _breweryDao;
	private int _createdProductId;
	private int _createdCategoryId;
	private int _createdBreweryId;
	private string _testSuffix = "_Test";

	[SetUp]
	public async Task SetUpAsync()
	{
		_productDao = new ProductDAO(Configuration.ConnectionString());
		_categoryDao = new CategoryDAO(Configuration.ConnectionString());
		_breweryDao = new BreweryDAO(Configuration.ConnectionString());


		var category = new Category
		{
			Name = $"IPA{_testSuffix}",
			IsDeleted = false
		};

		_createdCategoryId = await _categoryDao.CreateCategoryAsync(category);

		var brewery = new Brewery
		{
			Name = $"Overtone{_testSuffix}",
			IsDeleted = false
		};

		_createdBreweryId = await _breweryDao.CreateBreweryAsync(brewery);

		var product = new Product
		{
			Name = $"All that jazz{_testSuffix}",
			CategoryId_FK = _createdCategoryId,
			BreweryId_FK = _createdBreweryId,
			Price = 75f,
			Description = "Banana.",
			Stock = 10,
			Abv = 8.5f,
			ImageUrl = "http://example.com/image.jpg",
			IsDeleted = false
		};

		_createdProductId = await _productDao.CreateAsync(product);
	}

	[TearDown]
	public async Task TearDownAsync()
	{
		if (_createdProductId != 0)
		{
			await _productDao.DeleteAsync(_createdProductId);
		}

		if (_createdCategoryId != 0)
		{
			await _categoryDao.DeleteAsync(_createdCategoryId);
		}

		if (_createdBreweryId != 0)
		{
			await _breweryDao.DeleteAsync(_createdBreweryId);
		}
	}

	[Test]
	public async Task GetByIdAsync_WhenProductExist_ShouldReturnProduct()
	{
		var product = await _productDao.GetByIdAsync(_createdProductId);

		Assert.IsNotNull(product);
		Assert.That(product.Id, Is.EqualTo(_createdProductId));
		Assert.That(product.Name, Is.EqualTo($"All that jazz{_testSuffix}"));
	}

	[Test]
	public async Task CreateAsync_WhenCreated_ShouldReturnId()
	{
		var product = new Product
		{
			Name = $"Jazz Hands{_testSuffix}",
			CategoryId_FK = _createdCategoryId,
			BreweryId_FK = _createdBreweryId,
			Price = 80f,
			Description = "Citrusy flavor.",
			Stock = 5,
			Abv = 6.5f,
			ImageUrl = "http://example.com/image2.jpg",
			IsDeleted = false
		};

		var createdProductId = await _productDao.CreateAsync(product);

		Assert.Greater(createdProductId, 0, "The returned product ID should be greater than 0.");

		var createdProduct = await _productDao.GetByIdAsync(createdProductId);
		Assert.IsNotNull(createdProduct, "The created product should not be null.");
		Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
		Assert.That(createdProduct.CategoryId_FK, Is.EqualTo(product.CategoryId_FK));
		Assert.That(createdProduct.BreweryId_FK, Is.EqualTo(product.BreweryId_FK));
		Assert.That(createdProduct.Price, Is.EqualTo(product.Price));
		Assert.That(createdProduct.Description, Is.EqualTo(product.Description));
		Assert.That(createdProduct.Stock, Is.EqualTo(product.Stock));
		Assert.That(createdProduct.Abv, Is.EqualTo(product.Abv));
		Assert.That(createdProduct.ImageUrl, Is.EqualTo(product.ImageUrl));
		Assert.That(createdProduct.IsDeleted, Is.EqualTo(product.IsDeleted));

		await _productDao.DeleteAsync(createdProductId);
	}

	[Test]
	public async Task GetProductCategoriesAsync_WhenTestCategoriesExist_ShouldReturnOnlyTestCategories()
	{
		var categories = (await _productDao.GetProductCategoriesAsync());

		Assert.IsNotNull(categories, "The categories should not be null.");
		Assert.That(categories.Count, Is.GreaterThan(0), "The number of test categories should match the expected count.");
		Assert.That(categories, Contains.Item($"IPA{_testSuffix}"), $"The category 'IPA{_testSuffix}' should be present in the list.");


	}
}
