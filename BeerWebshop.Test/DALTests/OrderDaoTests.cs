﻿using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class OrderDaoTests
{
    private OrderDAO _orderDao;
    private ProductDAO _productDao;
    private BreweryDAO _breweryDao;
    private CategoryDAO _categoryDao;
    private AccountDAO _accountDao;

    private readonly string _testSuffix = "_Test";

    private int _testBreweryId;
    private int _testCategoryId;
    private int _testProductId;
    private int _testOrderId;
    private int _testCustomerId;
    private Customer _testCustomer;

    [SetUp]
    public async Task SetUp()
    {
        var connectionString = DBConnection.ConnectionString();

        _accountDao = new AccountDAO(connectionString);
        _orderDao = new OrderDAO(connectionString);
        _productDao = new ProductDAO(connectionString);
        _breweryDao = new BreweryDAO(connectionString);
        _categoryDao = new CategoryDAO(connectionString);

        _testCategoryId = await _categoryDao.CreateAsync(new Category { Name = $"Category{_testSuffix}", IsDeleted = false });
        _testBreweryId = await _breweryDao.CreateAsync(new Brewery { Name = $"Brewery{_testSuffix}", IsDeleted = false });

        _testCustomer = new Customer
        {
            Name = $"Test{_testSuffix} Test{_testSuffix}",
            Phone = "12345678",
            PasswordHash = "password",
            Age = 20,
            Email = $"test{_testSuffix}@example.com",
            Address = "Møllevej 2 9000 Aalborg"
        };

        _testCustomerId = await _accountDao.CreateAsync(_testCustomer);
        _testCustomer = await _accountDao.GetByIdAsync(_testCustomerId);

        _testProductId = await _productDao.CreateAsync(new Product
        {
            Name = $"Product{_testSuffix}",
            Category = await _categoryDao.GetByIdAsync(_testCategoryId),
            Brewery = await _breweryDao.GetByIdAsync(_testBreweryId),
            Price = 50f,
            Description = "Test product description.",
            Stock = 5,
            Abv = 5.0f,
            ImageUrl = "http://example.com/test.jpg",
            IsDeleted = false
        });
    }

    [Test]
    public async Task CreateAsync_WhenCalled_ShouldInsertOrderAndOrderLinesCorrectly()
    {
        var product = await _productDao.GetByIdAsync(_testProductId);

        var quantity = 2;
        var expectedTotal = quantity * product!.Price;

        var order = new Order
        {
            CreatedAt = DateTime.Now,
            DeliveryAddress = "Smith Residence",
            IsDelivered = false,
            Customer = _testCustomer,
            IsDeleted = false,
            OrderLines = new List<OrderLine>
            {
                new OrderLine { Quantity = quantity, Product = product }
            }
        };

        _testOrderId = await _orderDao.CreateAsync(order);

        Assert.That(_testOrderId, Is.GreaterThan(0), "Order ID should be a positive integer.");

        using var connection = new SqlConnection(DBConnection.ConnectionString());
        var orderLineData = await connection.QuerySingleAsync<(int Count, decimal SumTotal)>(
            "SELECT COUNT(1) AS Count, SUM(Total) AS SumTotal FROM OrderLines WHERE OrderId = @OrderId AND ProductId = @ProductId",
            new { OrderId = _testOrderId, ProductId = _testProductId });

        Assert.That(orderLineData.Count, Is.EqualTo(1), "There should be exactly one order line for the order and product.");
        Assert.That(orderLineData.SumTotal, Is.EqualTo(expectedTotal).Within(0.01), "The order line total should match the expected calculated price.");
    }

    [TearDown]
    public async Task TearDown()
    {
        // Ensure dependent data is cleaned up first
        using var connection = new SqlConnection(DBConnection.ConnectionString());
        await connection.ExecuteAsync("DELETE FROM OrderLines WHERE OrderId = @OrderId", new { OrderId = _testOrderId });

        // Delete entities in reverse order of dependency
        await _orderDao.DeleteAsync(_testOrderId);
        await _productDao.DeleteAsync(_testProductId);
        await _breweryDao.DeleteAsync(_testBreweryId);
        await _categoryDao.DeleteAsync(_testCategoryId);
        await _accountDao.DeleteAsync(_testCustomerId);
    }
}

