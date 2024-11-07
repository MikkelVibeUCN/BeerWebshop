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
        _productDao = new ProductDAO(Configuration.ConnectionString());
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        if (_createdProductId > 0)
        {
            await _productDao.DeleteAsync(_createdProductId);
            _createdProductId = 0;
        }
    }

    [Test]
    public async Task GetByIdAsync_WhenProductExist_ShouldReturnProduct()
    {
        int existingProductId = 27;

        var product = await _productDao.GetByIdAsync(existingProductId);

        Assert.IsNotNull(product, "Product show not be null");
        Assert.That(product.Id, Is.EqualTo(existingProductId), "Product id should be the same as the id we requested");
    }

    [Test]
    public async Task CreateAsync_WhenCreated_ShouldReturnId()
    {
        // Arrange
        var category = new Category { Id = 19, Name = "Sample Category" };
        var brewery = new Brewery { Id = 19, Name = "Sample Brewery" };

        var product = new Product
        {
            Name = "All that jazz",
            Brewery = brewery,
            Category = category,
            Price = 75f,
            Description = "Banana.",
            Stock = 10,
            Abv = 8.5f,
            ImageUrl = "http://example.com/image.jpg",
            IsDeleted = false
        };

        // Act
        var createdProductId = await _productDao.CreateAsync(product);

        // Assert
        Assert.Greater(createdProductId, 0, "The returned product ID should be greater than 0.");

        var createdProduct = await _productDao.GetByIdAsync(createdProductId);
        Assert.IsNotNull(createdProduct, "The created product should not be null.");
        Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
        Assert.That(createdProduct.Brewery.Id, Is.EqualTo(product.Brewery.Id));
        Assert.That(createdProduct.Price, Is.EqualTo(product.Price));
        Assert.That(createdProduct.Description, Is.EqualTo(product.Description));
        Assert.That(createdProduct.Stock, Is.EqualTo(product.Stock));
        Assert.That(createdProduct.Abv, Is.EqualTo(product.Abv));
        Assert.That(createdProduct.Category.Id, Is.EqualTo(product.Category.Id));
        Assert.That(createdProduct.ImageUrl, Is.EqualTo(product.ImageUrl));
        Assert.That(createdProduct.IsDeleted, Is.EqualTo(product.IsDeleted));
    }
}
