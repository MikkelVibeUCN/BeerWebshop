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
            Password = "æggemad",
            Phone = "60170091",
            Age = 18
        };

        int customerId = await _accountDAO.SaveCustomerAsync(customer);

        _customersCreated.Add(customerId);

        var customerFound = await _accountDAO.GetCustomerByIdAsync(customerId);

        Assert.That(customer != null);
        Assert.That(customer.Id == customerFound.Id);
        Assert.That(customer.Name == customerFound.Name);
        Assert.That(customer.Address.Equals(customerFound.Address));
        Assert.That(customer.Email == customerFound.Email);
        Assert.That(!customer.Password.Equals(customerFound.Password));
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
            Password = "æggemad",
            Phone = "60170091",
            Age = 18
        };
        int customerId = await _accountDAO.SaveCustomerAsync(customer);
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
            Password = "æggemad",
            Phone = "60170091",
            Age = 18
        };

        Assert.ThrowsAsync<Exception>(async () =>
        {
            int customerId = await _accountDAO.SaveCustomerAsync(customer);
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
            Password = "æggemad",
            Phone = "60170091",
            Age = 18
        };


        int customerId = await _accountDAO.SaveCustomerAsync(customer);
        _customersCreated.Add(customerId);

        var customer2 = new Customer()
        {
            Name = "Efternavn navn",
            Address = "vej 15 9000 aalborg",
            Email = "123@123.com",
            Password = "æggemad",
            Phone = "12345678",
            Age = 18
        };

        Assert.ThrowsAsync<Exception>(async () =>
        {
            int customerId = await _accountDAO.SaveCustomerAsync(customer2);
            _customersCreated.Add(customerId);
        });
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
            await _accountDAO.DeleteCustomerAsync(id);
        }
    }
}
