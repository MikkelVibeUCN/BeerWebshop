using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;

namespace BeerWebshop.Test.RestServicesTests;

[TestFixture]
public class ProductServiceTests
{
    private ProductService _productService;
    private CategoryService _categoryService;
    private BreweryService _breweryService;
    private string _connectionString = DBConnection.ConnectionString();

    private int _createdProductId;

    [SetUp]
    public async Task SetUp()
    {
        var productDao = new ProductDAO(_connectionString);
        var categoryDao = new CategoryDAO(_connectionString);
        var breweryDao = new BreweryDAO(_connectionString);

        _categoryService = new CategoryService(categoryDao);
        _breweryService = new BreweryService(breweryDao);
        _productService = new ProductService(productDao, _categoryService, _breweryService);
    }

    [Test]
    public async Task CreateProductAsync_WhenProductIsValid_ShouldReturnProductId()
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
            Name = "TestProduct",
            BreweryName = "TestBrewery",
            Price = 10.0f,
            Description = "A test product",
            Stock = 50,
            ABV = 5.0f,
            CategoryName = "TestCategory",
            ImageUrl = "https://example.com/sample-image.jpg",
            RowVersion = ""
        };

        _createdProductId = await _productService.CreateProductAsync(testProductDTO);
       
        Assert.That(_createdProductId, Is.GreaterThan(0), "The returned product ID should be greater than 0.");

        var createdProduct = await _productService.GetProductByIdAsync(_createdProductId);
        Assert.That(createdProduct, Is.Not.Null, "The created product should not be null.");
        Assert.That(createdProduct.Name, Is.EqualTo("TestProduct"), "The product name should match the input.");

        await _breweryService.DeleteBreweryAsync(breweryId);
        await _categoryService.DeleteCategoryAsync(categoryId); 
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
