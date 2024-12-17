using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.Test.Stubs;

namespace BeerWebshop.Test.RestServicesTests;

[TestFixture]
public class ProductServiceUnitTestt
{
    private ProductService _productService;
    private IProductDAO _productDAO;
    private ICategoryService _categoryService;
    private IBreweryService _breweryService;


    [SetUp]
    public void SetUp()
    {
        _productDAO = new StubProductDAO();
        _categoryService = new StubCategoryService();
        _breweryService = new StubBreweryService();
        _productService = new ProductService(_productDAO, _categoryService, _breweryService);
    }

    [Test]
    public async Task CreateProductAsync_WhenProductIsValid_ShouldReturnProductId()
    {
        // Arrange
        var productDTO = new ProductDTO
        {
            Name = "Thomas",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            ABV = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            CategoryName = "IPA",
            BreweryName = "Overtone A",
            RowVersion = ""
        };

        // Act
        var productId = await _productService.CreateProductAsync(productDTO);

        // Assert
        var createdProduct = await _productDAO.GetByIdAsync(productId);
        Assert.IsNotNull(createdProduct);
        Assert.AreEqual(productDTO.Name, createdProduct.Name);
    }

    [Test]
    public async Task GetProductByIdAsync_ShouldReturnProductDTO()
    {
        //Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Thomas",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            Abv = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            Category = new Category { Name = "IPA" },
            Brewery = new Brewery { Name = "Overtone" },
        };

        await _productDAO.CreateAsync(product);

        //Act
        var productDTO = await _productService.GetProductByIdAsync(1);

        //Assert
        Assert.IsNotNull(productDTO);
        Assert.AreEqual(product.Name, productDTO.Name);
    }

    [Test]
    public async Task UpdateProductAsync_ShouldUpdateProduct()
    {
        //Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Thomas",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            Abv = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            Category = new Category { Name = "IPA" },
            Brewery = new Brewery { Name = "Overtone" },
        };
        await _productDAO.CreateAsync(product);

        var updatedDTO = new ProductDTO
        {
            Id = 1,
            Name = "Updated Thomas",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            ABV = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            CategoryName = "IPA",
            BreweryName = "Overtone",
            RowVersion = ""
        };

        //Act
        var result = await _productService.UpdateProductAsync(updatedDTO);

        //Assert
        Assert.IsTrue(result);
        var updatedProduct = await _productDAO.GetByIdAsync(1);
        Assert.AreEqual(updatedDTO.Name, updatedProduct.Name);
    }

    [Test]
    public async Task DeleteProductByIdAsync_ShouldRemoveProduct()
    {
        //Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Thomas",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            Abv = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            Category = new Category { Name = "IPA" },
            Brewery = new Brewery { Name = "Overtone" },
        };

        await _productDAO.CreateAsync(product);

        //Act
        var result = await _productService.DeleteProductByIdAsync(1);

        //Assert
        Assert.IsTrue(result);
        var deletedProduct = await _productDAO.GetByIdAsync(1);
        Assert.IsNull(deletedProduct);
    }
}
