﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Properties;
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
    private int _createdProductId;
    private int _createdCategoryId;
    private int _createdBreweryId;
    private int _createdCustomerId;

    [SetUp]
    public async Task SetUp()
    {
        var productDao = new ProductDAO(_connectionString);
        var orderDao = new OrderDAO(_connectionString);
        var categoryDao = new CategoryDAO(_connectionString);
        var breweryDao = new BreweryDAO(_connectionString);
        var accountDao = new AccountDAO(_connectionString);

        JWTSettings jwtSetting = new JWTSettings
        {
            SecretKey = "YourSuperSecretKeyForJWTGeneration123!",
            Issuer = "BeerWebshop",
            Audience = "BeerWebshopUsers",
            ExpirationMinutes = 60
        };

        _categoryService = new CategoryService(categoryDao);
        _breweryService = new BreweryService(breweryDao);
        _productService = new ProductService(productDao, _categoryService, _breweryService);
        _orderService = new OrderService(orderDao, _productService, _connectionString);
        _accountService = new AccountService(accountDao, new JWTService(jwtSetting));

    }

    [Test]
    public async Task CreateOrderAsync_WhenOrderIsValid_ShouldReturnOrderId()
    {
        //Arrange
        _createdBreweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
        });
        var testBrewery = await _breweryService.GetBreweryById(_createdBreweryId);

        _createdCategoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
        });
        var testCategory = await _categoryService.GetCategoryById(_createdCategoryId);

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

        _createdProductId = await _productService.CreateProductAsync(testProductDTO);
        var testProduct = await _productService.GetProductEntityByIdAsync(_createdProductId);

        Customer customer = new Customer
        {
            Name = $"Test Test",
            Phone = "12345678",
            PasswordHash = "password",
            Age = 20,
            Email = "test@test.dk",
            Address = "TestStreet 10 9000 aalborg",
            Role = "User"

        };

        _createdCustomerId = await _accountService.SaveCustomerAsync(customer);

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
        //Act
        _createdOrderId = await _orderService.CreateOrderAsync(testOrder);
        //Assert
        Assert.That(_createdOrderId, Is.GreaterThan(0), "The returned order ID should be greater than 0.");

        var updatedProduct = await _productService.GetProductByIdAsync(testProduct.Id ?? 0);
        Assert.That(updatedProduct.Stock, Is.EqualTo(testProduct.Stock - 2), "The product stock should be reduced by the order quantity.");
    }

    [Test]
    public async Task CreateOrderAsync_WhenProductStockIsInsufficient_ShouldThrowInvalidOperationException()
    {
        //Arrange
        _createdBreweryId = await _breweryService.CreateBreweryAsync(new Brewery
        {
            Name = "TestBrewery",
        });
        var testBrewery = await _breweryService.GetBreweryById(_createdBreweryId);

        _createdCategoryId = await _categoryService.CreateCategoryAsync(new Category
        {
            Name = "TestCategory",
        });
        var testCategory = await _categoryService.GetCategoryById(_createdCategoryId);

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

        _createdProductId = await _productService.CreateProductAsync(testProductDTO);
        var testProduct = await _productService.GetProductEntityByIdAsync(_createdProductId);

        Customer customer = new Customer
        {
            Name = $"Test Test",
            Phone = "12345678",
            PasswordHash = "password",
            Age = 20,
            Email = "test@test.dk",
            Address = "Street 10 9000 aalborg",
            Role = "User"

        };

        _createdCustomerId = await _accountService.SaveCustomerAsync(customer);

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
        //Act
        await _orderService.UpdateStockAsync(_createdProductId, 3);
        //Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _orderService.CreateOrderAsync(testOrder));

        Assert.That(ex.Message, Does.Contain("Insufficient stock"));
    }


    [TearDown]
    public async Task TearDown()
    {
        if (_createdOrderId > 0)
        {
            await _orderService.DeleteOrderByIdAsync(_createdOrderId);
            _createdOrderId = 0;
        }

        await _productService.DeleteProductByIdAsync(_createdProductId);
        await _breweryService.DeleteBreweryAsync(_createdBreweryId);
        await _categoryService.DeleteCategoryAsync(_createdCategoryId);
        await _accountService.DeleteCustomer(_createdCustomerId);
    }
}
