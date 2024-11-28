using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;

namespace BeerWebshop.Test.RestServicesTests;

[TestFixture]
public class OrderServiceTests
{
    private OrderService _orderService;
    private ProductService _productService;
    private string _connectionString = DBConnection.ConnectionString();
    private CategoryService _categoryService;
    private BreweryService _breweryService;
    private AccountService _accountService;

    private int _createdOrderId;

    [SetUp]
    public async Task SetUp()
    {
        var productDao = new ProductDAO(_connectionString);
        var orderDao = new OrderDAO(_connectionString);
        var categoryDao = new CategoryDAO(_connectionString);
        var breweryDao = new BreweryDAO(_connectionString);
        var accountDao = new AccountDAO(_connectionString);

        _accountService = new AccountService(accountDao);
        _categoryService = new CategoryService(categoryDao);
        _breweryService = new BreweryService(breweryDao);
        _productService = new ProductService(productDao, _categoryService, _breweryService);
        _orderService = new OrderService(orderDao, _productService, _connectionString);
    }

    [Test]
    public async Task CreateOrderAsync_WhenOrderIsValid_ShouldReturnOrderId()
    {
        var breweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
            IsDeleted = false
        });
        var testBrewery = await _breweryService.GetBreweryById(breweryId);

        var categoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
            IsDeleted = false
        });
        var testCategory = await _categoryService.GetCategoryById(categoryId);

        var testProductDTO = new ProductDTO
        {
            Name = "TestProduct",
            BreweryName = testBrewery.Name,
            Price = 10.0f,
            Description = "Test",
            Stock = 10,
            ABV = 5.0f,
            CategoryName = testCategory.Name,
            ImageUrl = "https://example.com/sample-image.jpg",
            RowVersion = ""
        };

        var productId = await _productService.CreateProductAsync(testProductDTO);
        var testProduct = await _productService.GetProductEntityByIdAsync(productId);

        Customer customer = new Customer
        {
            Name = $"Test Test",
            Phone = "12345678",
            Password = "password",
            Age = 20,
            Email = $"dsajkdkjjhad@dsasdaad",
            Address = "Street number 9000 aalborg"
        };

        int customerId = await _accountService.SaveCustomerAsync(customer);

        var testOrder = new Order
        {
            CreatedAt = DateTime.Now,
            DeliveryAddress = "123 Smiths Residence",
            IsDelivered = false,
            Customer = customer,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    Quantity = 2,
                    Product = testProduct
                }
            }
        };

        _createdOrderId = await _orderService.CreateOrderAsync(testOrder);

        Assert.That(_createdOrderId, Is.GreaterThan(0), "The returned order ID should be greater than 0.");

        var updatedProduct = await _productService.GetProductByIdAsync(testProduct.Id ?? 0);
        Assert.That(updatedProduct.Stock, Is.EqualTo(testProduct.Stock - 2), "The product stock should be reduced by the order quantity.");

        await _orderService.DeleteOrderByIdAsync(_createdOrderId);
        await _productService.DeleteProductByIdAsync(productId);
        await _breweryService.DeleteBreweryAsync(breweryId);
        await _categoryService.DeleteCategoryAsync(categoryId);
        await _accountService.DeleteCustomer(customerId);
    }

    [Test]
    public async Task CreateOrderAsync_WhenProductStockIsInsufficient_ShouldThrowInvalidOperationException()
    {
        var breweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
            IsDeleted = false
        });
        var testBrewery = await _breweryService.GetBreweryById(breweryId);

        var categoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
            IsDeleted = false
        });
        var testCategory = await _categoryService.GetCategoryById(categoryId);

        var testProductDTO = new ProductDTO
        {
            Name = "TestProduct",
            BreweryName = testBrewery.Name,
            Price = 10.0f,
            Description = "Test",
            Stock = 5,
            ABV = 5.0f,
            CategoryName = testCategory.Name,
            ImageUrl = "https://example.com/sample-image.jpg",
            RowVersion = ""
        };

        var productId = await _productService.CreateProductAsync(testProductDTO);
        var testProduct = await _productService.GetProductEntityByIdAsync(productId);

        Customer customer = new Customer
        {
            Name = $"Test Test",
            Phone = "12345678",
            Password = "password",
            Age = 20,
            Email = $"dsajkdkjjhad@dsasdaad",
            Address = "Street number 9000 aalborg"
        };

        int customerId = await _accountService.SaveCustomerAsync(customer);

        var testOrder = new Order
        {
            CreatedAt = DateTime.Now,
            DeliveryAddress = "321 Smiths Residence",
            IsDelivered = false,
            Customer = customer,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    Quantity = 5,
                    Product = testProduct
                }
            }
        };

        await _productService.UpdateStockAsync(productId, 3);

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _orderService.CreateOrderAsync(testOrder));

        Assert.That(ex.Message, Does.Contain("Insufficient stock"));

        await _productService.DeleteProductByIdAsync(productId);
        await _breweryService.DeleteBreweryAsync(breweryId);
        await _categoryService.DeleteCategoryAsync(categoryId);
        await _accountService.DeleteCustomer(customerId);
    }


    [TearDown]
    public async Task TearDown()
    {
        if (_createdOrderId > 0)
        {
            await _orderService.DeleteOrderByIdAsync(_createdOrderId);
            _createdOrderId = 0;
        }
    }
}
