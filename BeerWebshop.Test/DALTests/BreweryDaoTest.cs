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
            // Arrange
            var breweryName = $"Brewery{_testSuffix}";
            var brewery = new Brewery { Name = breweryName, IsDeleted = false };

            // Act
            var breweryId = await _breweryDAO.CreateAsync(brewery);

            // Assert
            Assert.That(breweryId, Is.GreaterThan(0));

            // Test duplicate creation
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
            Assert.That(exception.Message, Is.EqualTo($"Brewery with name '{breweryName}' already exists."));

            // Cleanup
            await _breweryDAO.DeleteAsync(breweryId);
        }

        [Test]
        public async Task CreateAsync_WhenBreweryExists_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var breweryName = "Overtone";
            var brewery = new Brewery { Name = breweryName, IsDeleted = false };

            // Ensure brewery exists
            var existingBreweryId = await _breweryDAO.CreateAsync(brewery);

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
            Assert.That(exception.Message, Does.Contain($"Brewery with name '{breweryName}' already exists."));

            // Cleanup
            await _breweryDAO.DeleteAsync(existingBreweryId);
        }


    }
}
