using BeerWebshop.DAL.DATA.DAO.Stubs;
using BeerWebshop.DAL.DATA.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.DALTests.Unit_test
{
    public class ProductDAOUnitTest
    {
        private ProductDAOStub _productDAO;
        private Brewery _brewery;
        private Category _catergory;

        [SetUp]
        public async Task SetUp()
        {
            _productDAO = new ProductDAOStub();
            _brewery = new Brewery()
            {
                Id = 1,
                Name = "Test",
                IsDeleted = false,
            };
            _catergory = new Category()
            {
                Id = 1,
                Name = "Test",
                IsDeleted = false,
            };
        }


        [Test]
        public async Task CreateAsync_WhenProductIsCreated_ShouldReturnInt()
        {
            //Arrange
           
            var product = new Product
            {
                Name = "Citrus Breeze",
                Description = "A refreshing citrus-flavored beer with a hint of zest.",
                Price = 4.49f, 
                Stock = 40, 
                ImageUrl = null, 
                Abv = 5.5f, 
                RowVersion = new byte[8], 
                IsDeleted = false, 
                Category = _catergory, 
                Brewery = _brewery
            };
            //Act
            var result = await _productDAO.CreateAsync(product);
            //Assert
            Assert.That(result, Is.TypeOf<int>());
        }
        [Test]
        public async Task 
    }
}
