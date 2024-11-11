using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
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
        var connectionString = Configuration.ConnectionString();
        _accountDAO = new AccountDAO(connectionString);

        var customer = new Customer()
        {
            Id = _testId,
            Name = "Nikolaj",
            Address = "Smutvej 12",
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
        Assert.That(customer.Id == _testId);
        Assert.That(customer.Name == "Nikolaj");
    }
}
