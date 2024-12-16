using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class ProductDaoTests
{
    private ProductDAO _productDao;
    private CategoryDAO _categoryDao;
    private BreweryDAO _breweryDao;

    private readonly string _testSuffix = "_Test";

    private int _createdCategoryId;
    private int _createdBreweryId;

    private List<int> _productIdsCreated = new();
    private List<int> _breweryIdsCreated = new();
    private List<int> _categoryIdsCreated = new();

    [SetUp]
    public async Task SetUpAsync()
    {
        try
        {
            _productDao = new ProductDAO(DBConnection.ConnectionString());
            _categoryDao = new CategoryDAO(DBConnection.ConnectionString());
            _breweryDao = new BreweryDAO(DBConnection.ConnectionString());

            var category = new Category { Name = $"Category{_testSuffix}" };
            _createdCategoryId = await _categoryDao.CreateAsync(category);

            var brewery = new Brewery { Name = $"Brewery{_testSuffix}" };
            _createdBreweryId = await _breweryDao.CreateAsync(brewery);

        }
        catch (Exception ex)
        {
            throw;
        }


    }


    [TearDown]
    public async Task TearDownAsync()
    {
        try
        {
            await DeleteAllProductsCreated();
            await _categoryDao.DeleteAsync(_createdCategoryId);
            await _breweryDao.DeleteAsync(_createdBreweryId);
            _productIdsCreated.Clear();
        }
        catch (Exception ex)
        {
            throw;
        }


        //await DeleteAllCategoriesCreated();
        //await DeleteAllBreweriesCreated();
    }

    private async Task DeleteAllBreweriesCreated()
    {
        foreach (var id in _breweryIdsCreated)
        {
            await _breweryDao.DeleteAsync(id);
        }
    }

    private async Task DeleteAllCategoriesCreated()
    {
        foreach (var id in _categoryIdsCreated)
        {
            await _categoryDao.DeleteAsync(id);
        }
    }

    private async Task DeleteAllProductsCreated()
    {
        foreach (var productId in _productIdsCreated)
        {
            await _productDao.DeleteAsync(productId);
        }
    }

    [Test]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        var productId = await _productDao.CreateAsync(new Product
        {
            Name = $"Product{_testSuffix}",
            Category = new Category { Id = _createdCategoryId },
            Brewery = new Brewery { Id = _createdBreweryId },
            Price = 80f,
            Description = "Citrusy flavor.",
            Stock = 5,
            Abv = 6.5f,
            ImageUrl = "http://example.com/image.jpg"
        });

        _productIdsCreated.Add(productId);



        var product = await _productDao.GetByIdAsync(productId);

        Assert.IsNotNull(product);
        Assert.That(product.Id, Is.EqualTo(productId));
        Assert.That(product.Name, Is.EqualTo($"Product{_testSuffix}"));
    }

    [Test]
    public async Task CreateAsync_WhenCalled_ShouldCreateProduct()
    {
        var product = new Product
        {
            Name = $"Product{_testSuffix}",
            Category = new Category { Id = _createdCategoryId },
            Brewery = new Brewery { Id = _createdBreweryId },
            Price = 80f,
            Description = "Citrusy flavor.",
            Stock = 5,
            Abv = 6.5f,
            ImageUrl = "http://example.com/image.jpg",
        };

        var productId = await _productDao.CreateAsync(product);
        _productIdsCreated.Add(productId);


        var createdProduct = await _productDao.GetByIdAsync(productId);

        Assert.IsNotNull(createdProduct);
        Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
    }

    [Test]
    public async Task UpdateAsync_WhenProductUpdated_ShouldReflectChanges()
    {
        var productId = await _productDao.CreateAsync(new Product
        {
            Name = $"OriginalProduct{_testSuffix}",
            Category = new Category { Id = _createdCategoryId },
            Brewery = new Brewery { Id = _createdBreweryId },
            Price = 50f,
            Description = "Original description.",
            Stock = 10,
            Abv = 5.0f,
            ImageUrl = "http://example.com/original.jpg",
        });

        _productIdsCreated.Add(productId);


        var product = await _productDao.GetByIdAsync(productId);
        product.Name = $"UpdatedProduct{_testSuffix}";
        product.Price = 60f;

        var updateResult = await _productDao.UpdateAsync(product);

        Assert.IsTrue(updateResult);

        var updatedProduct = await _productDao.GetByIdAsync(productId);

        Assert.That(updatedProduct.Name, Is.EqualTo(product.Name));
        Assert.That(updatedProduct.Price, Is.EqualTo(product.Price));
    }

    [Test]
    public async Task UpdateAsync_WhenRowVersionModified_ShouldThrowConcurrencyException()
    {
        var productId = await _productDao.CreateAsync(new Product
        {
            Name = $"OriginalProduct{_testSuffix}",
            Category = new Category { Id = _createdCategoryId },
            Brewery = new Brewery { Id = _createdBreweryId },
            Price = 50f,
            Description = "Original description.",
            Stock = 10,
            Abv = 5.0f,
            ImageUrl = "http://example.com/original.jpg",
        });

        _productIdsCreated.Add(productId);


        var product = await _productDao.GetByIdAsync(productId);
        var originalRowVersion = product.RowVersion;

        product.Name = $"UpdatedProduct{_testSuffix}";
        await _productDao.UpdateAsync(product);

        product.RowVersion = originalRowVersion;

        var ex = Assert.ThrowsAsync<Exception>(async () => await _productDao.UpdateAsync(product));
        Assert.That(ex.Message, Is.EqualTo("Error updating product: Concurrency conflict detected."));
    }

    [Test]
    public async Task GetProductsAsync_WithAscendingNameOrder()
    {
        var queryParameters = new ProductQueryParameters { SortBy = "nameAsc" };
        var products = await _productDao.GetProducts(queryParameters);

        Assert.That(products, Is.Not.Null);
        Assert.That(products.Count(), Is.LessThanOrEqualTo(21));

        var sortedProducts = products.OrderBy(p => p.Name).ToList();
        Assert.That(products.SequenceEqual(sortedProducts));
    }

    [Test]
    public async Task GetProductsAsync_WithDescendingNameOrder()
    {
        var queryParameters = new ProductQueryParameters { SortBy = "nameDesc" };
        var products = await _productDao.GetProducts(queryParameters);

        Assert.That(products, Is.Not.Null);
        Assert.That(products.Count(), Is.LessThanOrEqualTo(21));

        var sortedProducts = products.OrderByDescending(p => p.Name).ToList();
        Assert.That(products.SequenceEqual(sortedProducts));
    }

    [Test]
    public async Task GetProductsAsync_WithAscendingPriceOrder()
    {
        var queryParameters = new ProductQueryParameters { SortBy = "priceAsc" };
        var products = await _productDao.GetProducts(queryParameters);

        Assert.That(products, Is.Not.Null);
        Assert.That(products.Count(), Is.LessThanOrEqualTo(21));

        var sortedProducts = products.OrderBy(p => p.Price).ToList();
        Assert.That(products.SequenceEqual(sortedProducts));
    }

    [Test]
    public async Task GetProductsAsync_WithDescendingPriceOrder()
    {
        var queryParameters = new ProductQueryParameters { SortBy = "priceDesc" };
        var products = await _productDao.GetProducts(queryParameters);

        Assert.That(products, Is.Not.Null);
        Assert.That(products.Count(), Is.LessThanOrEqualTo(21));

        var sortedProducts = products.OrderByDescending(p => p.Price).ToList();
        Assert.That(products.SequenceEqual(sortedProducts));
    }

    [Test]
    public async Task CreateAsync_WhenDuplicateName_ShouldThrowException()
    {
        var productName = $"DuplicateProduct{_testSuffix}";

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
        };

        var productId = await _productDao.CreateAsync(product);
        _productIdsCreated.Add(productId);


        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _productDao.CreateAsync(product));

        Assert.That(ex.Message, Does.Contain($"A product with the name '{productName}' already exists."));
    }
    [Test]
    public async Task DeleteAsync_WhenProductExists_ShouldRemoveProduct()
    {
        //Arrange
        var productName = $"DuplicateProduct{_testSuffix}";
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
        };
        //Act
        var productId = await _productDao.CreateAsync(product);
        _productIdsCreated.Add(productId);
        var deleteResult = await _productDao.DeleteAsync(productId);

        //Assert
        Assert.IsTrue(deleteResult, "DeleteAsync should return true when a product is successfully deleted.");
        var deletedProduct = await _productDao.GetByIdAsync(productId);
        Assert.IsNull(deletedProduct, "GetByIdAsync should return null for a deleted product.");
    }
}
