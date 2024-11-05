using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.Extensions.Configuration;

namespace BeerWebshop.Test.DALTests;

public class ProductDaoTests
{
    private ProductDAO _productDao;
    private int _createdProductId;

    [SetUp]
    public async Task SetUpAsync()
    {
        _productDao = new ProductDAO(Configuration.ConnectionString);
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        if (_createdProductId > 0)
        {
            await _productDao.DeleteByIdAsync(_createdProductId);
            _createdProductId = 0;
        }
    }

    [Test]
    public async Task GetByIdAsync_WhenProductExist_ShouldReturnProduct()
    {
        int existingProductId = 1;

        var product = await _productDao.GetByIdAsync(existingProductId);

        Assert.IsNotNull(product, "Product show not be null");
        Assert.That(product.Id, Is.EqualTo(existingProductId), "Product id should be the same as the id we requested");
    }

    [Test]
    public async Task CreateAsync_ShouldAddNewProductAndReturnId()
    {
        var product = new Product
        {
            Name = "Test Beer",
            Brewery = "Test Brewery",
            Price = 5.99f,
            Description = "A test beer for unit testing.",
            Stock = 10,
            ABV = 4.5f,
            Category = "Test Category"
        };

        _createdProductId = await _productDao.CreateAsync(product);

        Assert.Greater(_createdProductId, 0, "The returned product ID should be greater than 0.");

        var createdProduct = await _productDao.GetByIdAsync(_createdProductId);
        Assert.IsNotNull(createdProduct, "The created product should not be null.");
        Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
        Assert.That(createdProduct.Brewery, Is.EqualTo(product.Brewery));
        Assert.That(createdProduct.Price, Is.EqualTo(product.Price));
        Assert.That(createdProduct.Description, Is.EqualTo(product.Description));
        Assert.That(createdProduct.Stock, Is.EqualTo(product.Stock));
        Assert.That(createdProduct.ABV, Is.EqualTo(product.ABV));
        Assert.That(createdProduct.Category, Is.EqualTo(product.Category));
    }

    [Test]
    public async Task GetFromCategoryAsync_WhenCategoryIsIPA_ShouldReturnAllIPAProducts()
    {
        var productsFromCategory = (await _productDao.GetFromCategoryAsync("IPA")).ToList();

        Assert.IsTrue(productsFromCategory.Count >= 5, "There should be at least five product in the IPA category.");
        Assert.IsTrue(productsFromCategory.All(p => p.Category == "IPA"), "All products should be in the IPA category.");
    }
}
