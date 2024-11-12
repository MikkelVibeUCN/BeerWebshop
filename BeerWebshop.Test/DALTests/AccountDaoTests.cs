using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using Castle.Core.Resource;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class AccountDaoTests
{
    private AccountDAO _accountDAO;
    private int _testId = 1;
    [SetUp]
    public async Task SetUpAsync()
    {
        _accountDAO = new AccountDAO(Configuration.ConnectionString());
        var customer = new Customer()
        {
            Id = 1,
            Name = "Anders",
            Address = "1",
            ZipCode = "6969",
            City = "Lungeby",
            Email = "hej@dig.dk",
            Password = "æggemad",
            Phone = "60170091",
            Age = 18
        };

    }

    [Test]
    public async Task GetCustomerById_WhenCustomerExists_ShouldReturnCustomerWithGivenId()
    {
        var customer = await _accountDAO.GetCustomerByIdAsync(_testId);
        Assert.IsNotNull(customer);
        Assert.That(customer.Id == 1);
    }

    [Test]
    public async Task SaveCustomerAsync_WhenCalled_ShouldSaveCustomerAndReturnId()
    {

        var newCustomer = new Customer
        {
            Name = "Mads Stigers",
            Address = "Sthomasgadsasae",
            ZipCode = "6969",
            City = "Test City",
            Email = "testuser@example.com",
            Password = "TestPassword123",
            Phone = "1234567890",
            Age = 25
        };
        // Arrange
        var nameParts = newCustomer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        string firstName = nameParts.Length > 0 ? nameParts[0] : "";
        string lastName = nameParts.Length > 1
            ? string.Join(" ", nameParts.Skip(1))  // Join remaining parts as LastName if available
            : "";

        // Act
        int customerId = await _accountDAO.SaveCustomerAsync(newCustomer);

        // Assert
        Assert.That(customerId, Is.GreaterThan(0), "Customer ID should be greater than 0 indicating successful save.");

        // Additional check to verify customer is in the database (optional)
        var savedCustomer = await _accountDAO.GetCustomerByIdAsync(customerId);
        Assert.IsNotNull(savedCustomer, "Saved customer should not be null.");
        Assert.That(firstName, Is.EqualTo("Mads"));
        Assert.That(lastName, Is.EqualTo("Stigers"));
        Assert.That(savedCustomer.Email, Is.EqualTo("testuser@example.com"));
    }
}
