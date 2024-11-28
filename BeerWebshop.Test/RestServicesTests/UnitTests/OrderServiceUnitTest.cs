using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.DAO.Stubs;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Stubs;
using Castle.Core.Resource;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.RestServicesTests.UnitTests
{
    public class OrderServiceUnitTest
    {
        private OrderServiceStub _orderServiceStub;
        private ProductServiceStub _productService;

        [SetUp]
        public void SetUp()
        {
            _orderServiceStub = new OrderServiceStub();
            _productService = new ProductServiceStub();
        }

        [Test]
        public async Task CreateOrder_WhenOrderIsValid_ShouldReturnOrderID()
        {


            // Set up stubbed data
            Customer customer = new Customer
            {
                Name = $"Test Test",
                Phone = "12345678",
                PasswordHash = "00",
                Age = 20,
                Email = $"dsajkdkjjhad@dsasdaad",
                Address = "Street number 9000 aalborg"
            };


            var validProduct = new Product(
                id: 1,
                name: "Test Beer",
                category: new Category { Id = 1, Name = "IPA" },
                brewery: new Brewery { Id = 1, Name = "Test Brewery" },
                price: 12.99f,
                description: "A refreshing IPA with citrus and hop notes.",
                stock: 50,
                abv: 5.5f,
                imageUrl: "http://example.com/images/test-beer.jpg",
                isDeleted: false
            );

            var orderDto = new Order
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
                    Product = validProduct
                }
            }


            };


            // Act
            var result = await _orderServiceStub.CreateOrderAsyncStub(orderDto);

            // Assert
            Assert.IsInstanceOf<int>(result, "The returned value should be of type int.");
            Assert.AreEqual(1, result, "The returned order ID should match the expected value.");
        }
    }

}

