using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.DALTests
{
    public class BreweryDaoTest
    {
        private BreweryDAO _breweryDAO;
        private string _testSuffix = "_Test";

        [SetUp]
        public void SetUp()
        {
            _breweryDAO = new BreweryDAO(DBConnection.ConnectionString());
        }
        [Test]
        public async Task CreateAsync_WhenBreweryDoesNotExist_ShouldCreateBrewery()
        {
            
            var breweryName = $"Brewery{_testSuffix}";
            var brewery = new Brewery { Name = breweryName, IsDeleted = false };

            
            var breweryId = await _breweryDAO.CreateAsync(brewery);

            
            Assert.That(breweryId, Is.GreaterThan(0));

            
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
            Assert.That(exception.Message, Is.EqualTo($"Brewery with name '{breweryName}' already exists."));

            
            await _breweryDAO.DeleteAsync(breweryId);
        }

        [Test]
        public async Task CreateAsync_WhenBreweryExists_ShouldThrowInvalidOperationException()
        {
            
            var breweryName = "Overtone";
            var brewery = new Brewery { Name = breweryName, IsDeleted = false };

            
            var existingBreweryId = await _breweryDAO.CreateAsync(brewery);

            
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
            Assert.That(exception.Message, Does.Contain($"Brewery with name '{breweryName}' already exists."));

            
            await _breweryDAO.DeleteAsync(existingBreweryId);
        }


    }
}
