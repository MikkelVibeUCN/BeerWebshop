using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.DAO.Stubs;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Stubs;
using BeerWebshop.RESTAPI.Services.Interfaces;
using Castle.Core.Resource;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace BeerWebshop.Test.RestServicesTests.UnitTests
{
    public class OrderServiceUnitTest
    {
        private OrderService _orderService;
        private IProductService _productService;
        private IBreweryService _breweryService;
        private IAccountService _accountService;
        private ICategoryService _categoriaService;
        private IBreweryDAO _breweryDAO;
        private IOrderDAO _orderDAO;

        [SetUp]
        public void SetUp()
        {
            _orderDAO = new OrderDAOStub();
            _productService = new ProductServiceStub();
            _orderService = new OrderService(_orderDAO, _productService, string.Empty);

        }

        [Test]
        public async Task CreateOrder_WhenOrderIsValid_ShouldReturnOrderID()
        {
            //Arrange
            var customer = new CustomerDTO
            {
                Id = 1,
                Name = $"Test Test",
                Phone = "12345678",
                Age = 20,
                Address = "Street number 9000 aalborg"
            };


            var order = new OrderDTO
            {
                IsDelivered = false,
                CustomerDTO = customer,
                OrderLines = new List<OrderLineDTO>
    {
        new OrderLineDTO
        {
            Quantity = 2,
            Product = await _productService.GetProductByIdAsync(1)
        }
    }
            };
          

            // Act
            var orderId = await _orderService.CreateOrderFromDTOAsync(order);
            var createdOrder = await _orderDAO.GetByIdAsync(orderId);
            

            // Assert
            Assert.That(createdOrder, Is.Not.Null);
            Assert.IsInstanceOf<int>(orderId, "The returned value should be of type int.");
            Assert.AreEqual(1, orderId, "The returned order ID should match the expected value.");
            Assert.AreEqual(order.CustomerDTO.Name, createdOrder.Customer.Name);
            Assert.AreEqual(1, createdOrder.OrderLines.Count);
            Assert.AreEqual(200f, createdOrder.TotalPrice);
        }
    }

}

