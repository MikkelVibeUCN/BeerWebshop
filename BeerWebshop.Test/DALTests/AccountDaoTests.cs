using BeerWebshop.APIClientLibrary.ApiClient.DTO;
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
    private List<int> _customersCreated = new List<int>();
    [SetUp]
    public async Task SetUpAsync()
    {
        _accountDAO = new AccountDAO(DBConnection.ConnectionString());
     }

    [Test]
    public async Task GetCustomerById_WhenCustomerExists_ShouldReturnCustomerWithGivenId()
    {
        var customer = new Customer()
        {
            Name = "Navn efternavn",
            Address = "sejvej 11 9000 aalborg",
            Email = "hej@dig.dk",
            PasswordHash = "æggemad",
            Phone = "60170091",
            Age = 18
        };

        int customerId = await _accountDAO.CreateAsync(customer);

        _customersCreated.Add(customerId);

        var customerFound = await _accountDAO.GetByIdAsync(customerId);

        Assert.That(customer != null);
        Assert.That(customer.Id == customerFound.Id);
        Assert.That(customer.Name == customerFound.Name);
        Assert.That(customer.Address.Equals(customerFound.Address));
        Assert.That(customer.Email == customerFound.Email);
        Assert.That(!customer.PasswordHash.Equals(customerFound.PasswordHash));
        Assert.That(customer.Phone.Equals(customerFound.Phone));
    }

    [Test]
    public async Task SaveCustomerAsync_WhenCalled_ShouldSaveCustomerAndReturnId()
    {

        var customer = new Customer()
        {
            Name = "Navn efternavn",
            Address = "sejvej 11 9000 aalborg",
            Email = "hej@dig.dk",
            PasswordHash = "æggemad",
            Phone = "60170091",
            Age = 18
        };
        int customerId = await _accountDAO.CreateAsync(customer);
        _customersCreated.Add(customerId);
    }

    [Test]
    public async Task SaveCustomerAsync_WhenCalledWithNullEmail_ShouldThrowException()
    {
        var customer = new Customer()
        {
            Name = "Navn efternavn",
            Address = "sejvej 11 9000 aalborg",
            Email = null,
            PasswordHash = "æggemad",
            Phone = "60170091",
            Age = 18
        };

        Assert.ThrowsAsync<Exception>(async () =>
        {
            int customerId = await _accountDAO.CreateAsync(customer);
            _customersCreated.Add(customerId);
        });
    }

    [Test]

    public async Task SaveCustomerAsync_WhenCalledWithExistingEmail_ShouldThrowException()
    {
        var customer = new Customer()
        {
            Name = "Navn efternavn",
            Address = "sejvej 11 9000 aalborg",
            Email = "123@123.com",
            PasswordHash = "æggemad",
            Phone = "60170091",
            Age = 18
        };


        int customerId = await _accountDAO.CreateAsync(customer);
        _customersCreated.Add(customerId);

        var customer2 = new Customer()
        {
            Name = "Efternavn navn",
            Address = "vej 15 9000 aalborg",
            Email = "123@123.com",
            PasswordHash = "æggemad",
            Phone = "12345678",
            Age = 18
        };

        Assert.ThrowsAsync<Exception>(async () =>
        {
            int customerId = await _accountDAO.CreateAsync(customer2);
            _customersCreated.Add(customerId);
        });
    }

    [Test]
    public async Task CreateAddress_WhenCalled_ShouldSaveAddressToDatabase()
    {
        var customer = new Customer()
        {
            Name = "Test User",
            Address = "Testvej 10 1000 København",
            Email = "test@test.dk",
            PasswordHash = "testpassword",
            Phone = "12345678",
            Age = 30
        };

        int customerId = await _accountDAO.CreateAsync(customer);
        _customersCreated.Add(customerId);

        var customerFromDb = await _accountDAO.GetByIdAsync(customerId);

        Assert.That(customerFromDb.Address, Is.EqualTo("Testvej 10 1000 København"));
    }
    [Test]
    public async Task DeleteCustomerAsync_WhenCalled_ShouldRemoveCustomerFromDatabase()
    {
        var customer = new Customer()
        {
            Name = "Delete User",
            Address = "Deletevej 5 5000 Odense",
            Email = "delete@test.dk",
            PasswordHash = "deletepassword",
            Phone = "12345678",
            Age = 30
        };

        int customerId = await _accountDAO.CreateAsync(customer);
        _customersCreated.Add(customerId);

        bool isDeleted = await _accountDAO.DeleteAsync(customerId);

        Assert.That(isDeleted, Is.True);

        var customerFromDb = await _accountDAO.GetByIdAsync(customerId);
        Assert.That(customerFromDb, Is.Null);
    }
    [Test]
    public async Task CreateAddress_WhenZipCodeIsMissing_ShouldThrowException()
    {
        var customer = new Customer()
        {
            Name = "Zip User",
            Address = "NoZipvej 12",
            Email = "zip@test.dk",
            PasswordHash = "password",
            Phone = "12345678",
            Age = 30
        };

        var exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            int customerId = await _accountDAO.CreateAsync(customer);
            _customersCreated.Add(customerId);
        });

        Assert.That(exception.InnerException, Is.TypeOf<FormatException>());
        Assert.That(exception.InnerException.Message, Is.EqualTo("Address format is incorrect."));
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await DeleteAllCustomersMade();
    }

    private async Task DeleteAllCustomersMade()
    {
        foreach (var id in _customersCreated)
        {
            await _accountDAO.DeleteAsync(id);
        }
    }
}
