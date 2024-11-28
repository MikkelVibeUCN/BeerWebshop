using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.DAO.Stubs;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.RESTAPI.Stubs;

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
            Assert.That(orderId > 0);
            Assert.IsInstanceOf<int>(orderId, "The returned value should be of type int.");


        }



        [Test]
        public async Task GetOrderById_WhenOrderExists_ShouldReturnOrder()
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


            //Act
            var orderId = await _orderService.CreateOrderFromDTOAsync(order);
            var createdOrder = await _orderService.GetOrderByIdAsync(orderId);
            //Assert
            Assert.AreEqual(1, orderId, "The returned order ID should match the expected value.");
            Assert.AreEqual(order.CustomerDTO.Name, createdOrder.Customer.Name);
            Assert.AreEqual(1, createdOrder.OrderLines.Count);
            Assert.AreEqual(200f, createdOrder.TotalPrice);

        }

        [Test]
        public async Task GetOrderAsync_WhenOrdersExist_ShouldReturnAllOrder()
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
            var product = new ProductDTO()
            {
                Id = 1,
                Name = "test",
                BreweryName = "test",
                Price = 100,
                Description = "test",
                Stock = 10,
                ABV = 10,
                CategoryName = "test",
                ImageUrl = "pornhub.com",
                RowVersion = string.Empty,
            };


            var orderDTO = new OrderDTO
            {
                IsDelivered = false,
                CustomerDTO = customer,
                OrderLines = new List<OrderLineDTO>
                {
                    new OrderLineDTO
                    {
                        Quantity = 2,
                        Product = product
                    }
                }
            };
            //Act
            var orderid = await _orderService.CreateOrderFromDTOAsync(orderDTO);
            var orders = await _orderService.GetOrdersAsync();
            //Assert
            Assert.AreEqual(1, orders.Count());

        }

    }
}

