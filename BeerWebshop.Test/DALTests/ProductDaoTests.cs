﻿using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using System.Data;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class ProductDaoTests
{
	private ProductDAO _productDao;
	private CategoryDAO _categoryDao;
	private BreweryDAO _breweryDao;
	private int _createdCategoryId;
	private int _createdBreweryId;
	private string _testSuffix = "_Test";
    private List<int> _productIdsCreated = new List<int>();

    [SetUp]
	public async Task SetUpAsync()
	{
		_productDao = new ProductDAO(DBConnection.ConnectionString());
		_categoryDao = new CategoryDAO(DBConnection.ConnectionString());
		_breweryDao = new BreweryDAO(DBConnection.ConnectionString());

        _createdCategoryId = await _categoryDao.CreateAsync(new Category { Name = $"Category{_testSuffix}", IsDeleted = false });
        _createdBreweryId = await _breweryDao.CreateBreweryAsync(new Brewery { Name = $"Brewery{_testSuffix}", IsDeleted = false });
    }

    private async Task DeleteAllProductsMade()
    {
        foreach (var id in _productIdsCreated)
        {
            await _productDao.DeleteAsync(id);
        }
    }

    [TearDown]
	public async Task TearDownAsync()
	{
        await DeleteAllProductsMade();

        await _categoryDao.DeleteAsync(_createdCategoryId);
        await _breweryDao.DeleteAsync(_createdBreweryId);
    }

	[Test]
	public async Task GetByIdAsync_WhenProductExist_ShouldReturnProduct()
	{
        var createdProductId = await _productDao.CreateAsync(new Product
        {
            Name = $"All that jazz{_testSuffix}",
            Category = new Category { Id = _createdCategoryId },
            Brewery = new Brewery { Id = _createdBreweryId },
            Price = 80f,
            Description = "Citrusy flavor.",
            Stock = 5,
            Abv = 6.5f,
            ImageUrl = "http://example.com/image.jpg",
            IsDeleted = false
        });

        _productIdsCreated.Add(createdProductId);

        var product = await _productDao.GetByIdAsync(createdProductId);

		Assert.IsNotNull(product);
		Assert.That(product.Id == createdProductId);
		Assert.That(product.Name.Equals($"All that jazz{_testSuffix}"));
	}



	[Test]
	public async Task CreateAsync_WhenCreated_ShouldReturnId()
	{
		var product = new Product
		{
			Name = $"Jazz Hands{_testSuffix}",
			Category = new Category { Id = _createdCategoryId},
			Brewery = new Brewery { Id = _createdBreweryId },
            Price = 80f,
			Description = "Citrusy flavor.",
			Stock = 5,
			Abv = 6.5f,
			ImageUrl = "http://example.com/image2.jpg",
			IsDeleted = false
		};
		var createdProductId = await _productDao.CreateAsync(product);

        _productIdsCreated.Add(createdProductId);

        Assert.Greater(createdProductId, 0, "The returned product ID should be greater than 0.");

		var createdProduct = await _productDao.GetByIdAsync(createdProductId);
		Assert.IsNotNull(createdProduct, "The created product should not be null.");
		Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
		Assert.That(createdProduct.Category.Id == product.Category.Id);
		Assert.That(createdProduct.Brewery.Id == product.Brewery.Id);
		Assert.That(createdProduct.Price, Is.EqualTo(product.Price));
		Assert.That(createdProduct.Description, Is.EqualTo(product.Description));
		Assert.That(createdProduct.Stock, Is.EqualTo(product.Stock));
		Assert.That(createdProduct.Abv, Is.EqualTo(product.Abv));
		Assert.That(createdProduct.ImageUrl, Is.EqualTo(product.ImageUrl));
		Assert.That(createdProduct.IsDeleted, Is.EqualTo(product.IsDeleted));

		await _productDao.DeleteAsync(createdProductId);
	}

	[Test]
	public async Task CreateAsync_WhenProductWithSameNameExists_ShouldThrowInvalidOperationException()
	{
		var productName = $"Product{_testSuffix}";
		var product = new Product
		{
			Name = productName,
			Category = new Category { Id = _createdCategoryId },
			Brewery = new Brewery { Id = _createdBreweryId },
			Price = 80f,
			Description = "Citrusy flavor.",
			Stock = 5,
			Abv = 6.5f,
			ImageUrl = "http://example.com/image.jpg",
			IsDeleted = false
		};

		var createdProductId = await _productDao.CreateAsync(product);
		_productIdsCreated.Add(createdProductId);

		var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _productDao.CreateAsync(product));

		Assert.That(ex.Message, Does.Contain($"A product with the name '{productName}' already exists."));
	}


	[Test]
	public async Task GetProductCategoriesAsync_WhenTestCategoriesExist_ShouldReturnOnlyTestCategories()
	{
		var categories = (await _productDao.GetProductCategoriesAsync());

		Assert.IsNotNull(categories, "The categories should not be null.");
		Assert.That(categories.Count, Is.GreaterThan(0), "The number of test categories should match the expected count.");
		Assert.That(categories, Contains.Item($"Category{_testSuffix}"), $"The category 'Category{_testSuffix}' should be present in the list.");
	}

	[Test]
    public async Task GetProductsAsync_WithAscendingNameOrder()
    {
        ProductQueryParameters productQueryParameters = new ProductQueryParameters
        {
            SortBy = "nameAsc"
        };

        var products = await _productDao.GetProducts(productQueryParameters);

        Assert.That(products != null, "The products should not be null.");
        Assert.That(products?.Count() <= 21, "The number of products should match the expected count.");

        var sortedProducts = products.OrderBy(p => p.Name);
        Assert.That(products.SequenceEqual(sortedProducts), "The products should be sorted in ascending order by name.");
    }

    [Test]
    public async Task GetProductsAsync_WithDescendingNameOrder()
    {
        ProductQueryParameters productQueryParameters = new ProductQueryParameters
        {
            SortBy = "nameDesc"
        };

        var products = await _productDao.GetProducts(productQueryParameters);

        Assert.That(products != null, "The products should not be null.");
        Assert.That(products!.Count(), Is.LessThanOrEqualTo(21), "The number of products should match the expected count.");

        var sortedProducts = products!.OrderByDescending(p => p.Name);
        Assert.That(products!.SequenceEqual(sortedProducts), "The products should be sorted in descending order by name.");
    }

    [Test]
    public async Task GetProductsAsync_WithAscendingPriceOrder()
    {
        ProductQueryParameters productQueryParameters = new ProductQueryParameters
        {
            SortBy = "priceAsc"
        };
        var products = await _productDao.GetProducts(productQueryParameters);

        Assert.That(products != null, "The products should not be null.");
        Assert.That(products!.Count(), Is.LessThanOrEqualTo(21), "The number of products should match the expected count.");

        var sortedProducts = products!.OrderBy(p => p.Price);
        Assert.That(products!.SequenceEqual(sortedProducts), "The products should be sorted in ascending order by price.");
    }

    [Test]
    public async Task GetProductsAsync_WithDescendingPriceOrder()
    {
        ProductQueryParameters productQueryParameters = new ProductQueryParameters
        {
            SortBy = "priceDesc"
        };
        var products = await _productDao.GetProducts(productQueryParameters);

        Assert.That(products != null, "The products should not be null.");
        Assert.That(products!.Count(), Is.LessThanOrEqualTo(21), "The number of products should match the expected count.");

        var sortedProducts = products!.OrderByDescending(p => p.Price);
        Assert.That(products!.SequenceEqual(sortedProducts), "The products should be sorted in descending order by price.");
    }

	[Test]
	public async Task UpdateAsync_WhenProductIsUpdated_ShouldReturnTrueAndUpdateFields()
	{
		var createdProductId = await _productDao.CreateAsync(new Product
		{
			Name = $"Original Name{_testSuffix}",
			Category = new Category { Id = _createdCategoryId },
			Brewery = new Brewery { Id = _createdBreweryId },
			Price = 50f,
			Description = "Original description.",
			Stock = 10,
			Abv = 5.0f,
			ImageUrl = "http://example.com/original.jpg",
			IsDeleted = false
		});
		_productIdsCreated.Add(createdProductId);

		var product = await _productDao.GetByIdAsync(createdProductId);
		Assert.IsNotNull(product);

		product.Name = $"Updated Name{_testSuffix}";
		product.Price = 60f;
		var updateResult = await _productDao.UpdateAsync(product);

		Assert.IsTrue(updateResult, "The product update should return true.");
		var updatedProduct = await _productDao.GetByIdAsync(createdProductId);
		Assert.That(updatedProduct.Name, Is.EqualTo(product.Name), "The product name should be updated.");
		Assert.That(updatedProduct.Price, Is.EqualTo(product.Price), "The product price should be updated.");
	}

	[Test]
	public async Task UpdateAsync_WhenRowVersionIsModified_ShouldThrowConcurrencyException()
	{
		var createdProductId = await _productDao.CreateAsync(new Product
		{
			Name = $"Original Name{_testSuffix}",
			Category = new Category { Id = _createdCategoryId },
			Brewery = new Brewery { Id = _createdBreweryId },
			Price = 50f,
			Description = "Original description.",
			Stock = 10,
			Abv = 5.0f,
			ImageUrl = "http://example.com/original.jpg",
			IsDeleted = false
		});
		_productIdsCreated.Add(createdProductId);

		var product = await _productDao.GetByIdAsync(createdProductId);
		var originalRowVersion = product.RowVersion;

		product.Name = $"Updated Name{_testSuffix}";
		product.Price = 60f;
		await _productDao.UpdateAsync(product);

		product.RowVersion = originalRowVersion;

		var ex = Assert.ThrowsAsync<Exception>(async () => await _productDao.UpdateAsync(product));
		Assert.That(ex.Message, Is.EqualTo("Error updating product: Concurrency conflict detected."));
	}



}
