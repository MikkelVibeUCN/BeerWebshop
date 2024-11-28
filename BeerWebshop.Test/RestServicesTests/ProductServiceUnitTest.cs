using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.Test.Stubs;

namespace BeerWebshop.Test.RestServicesTests;

[TestFixture]
public class ProductServiceUnitTestt
{
    private ProductService _productService;
    private StubProductDAO _productDAO;
    private StubCategoryService _categoryService;
    private StubBreweryService _breweryService;


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
            Name = "New Beer",
            Description = "A refreshing beer",
            Price = 4.99f,
            Stock = 100,
            Abv = 5.5f,
            ImageUrl = "https://example.com/beer.jpg",
            CategoryName = "Ale",
            BreweryName = "Brewery A"
        };

        // Act
        var productId = await _productService.CreateProductAsync(productDTO);

        // Assert
        var createdProduct = await _productDAO.GetByIdAsync(productId);
        Assert.IsNotNull(createdProduct);
        Assert.AreEqual(productDTO.Name, createdProduct.Name);
    }

    [Test]
    public async Task DeleteProductByIdAsync_WhenProductExists_ShouldDeleteProduct()
    {
        var breweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
            IsDeleted = false
        });

        var categoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
            IsDeleted = false
        });

        var testProductDTO = new ProductDTO
        {
            Name = "TestProductToDelete",
            BreweryName = "TestBrewery",
            Price = 15.0f,
            Description = "A product to delete",
            Stock = 20,
            ABV = 5.5f,
            CategoryName = "TestCategory",
            ImageUrl = "https://example.com/delete-image.jpg",
            RowVersion = ""
        };

        var productId = await _productService.CreateProductAsync(testProductDTO);
        var deleteResult = await _productService.DeleteProductByIdAsync(productId);

        Assert.That(deleteResult, Is.True, "The product deletion should return true.");
        var deletedProduct = await _productService.GetProductEntityByIdAsync(productId);
        Assert.That(deletedProduct, Is.Null, "The product should no longer exist in the database.");

        await _breweryService.DeleteBreweryAsync(breweryId);
        await _categoryService.DeleteCategoryAsync(categoryId);
    }

    [Test]
    public async Task UpdateStockAsync_WhenStockIsUpdated_ShouldReflectNewStock()
    {
        var breweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
            IsDeleted = false
        });

        var categoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
            IsDeleted = false
        });

        var testProductDTO = new ProductDTO
        {
            Name = "TestProductToUpdate",
            BreweryName = "TestBrewery",
            Price = 12.0f,
            Description = "A product to update stock",
            Stock = 30,
            ABV = 6.0f,
            CategoryName = "TestCategory",
            ImageUrl = "https://example.com/update-image.jpg",
            RowVersion = ""
        };

        _createdProductId = await _productService.CreateProductAsync(testProductDTO);

        var updateResult = await _productService.UpdateStockAsync(_createdProductId, 25);
        Assert.That(updateResult, Is.True, "Stock update should return true.");

        var updatedProduct = await _productService.GetProductByIdAsync(_createdProductId);
        Assert.That(updatedProduct.Stock, Is.EqualTo(5), "The product stock should be initial minus the updated value");

        await _breweryService.DeleteBreweryAsync(breweryId);
        await _categoryService.DeleteCategoryAsync(categoryId);
    }

    [TearDown]
    public async Task TearDown()
    {
        if (_createdProductId > 0)
        {
            await _productService.DeleteProductByIdAsync(_createdProductId);
            _createdProductId = 0;
        }
    }
}
