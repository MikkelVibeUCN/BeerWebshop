using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BeerWebshop.Test.DALTests
{
    public class OrderDaoTest
    {
        public OrderDao _orderDao;
        public ProductDAO _productDao;

        [SetUp]
        public async Task SetUpAsync()
        {
            _orderDao = new OrderDao(Configuration.ConnectionString());
            _productDao = new ProductDAO(Configuration.ConnectionString());

        }
        [Test]
        public async Task InsertOrderAsync_WhenOrderIsInserted_ShouldReturnOrderId()
        {
            // Arrange
            var order = new Order(DateTime.Now, new List<OrderLine>(), "123 Main St", false,1);

            var product = await _productDao.GetByIdAsync(27);
           
            var orderLine1 = new OrderLine(2,product,27);
            order.AddOrderLine(orderLine1);
            // Act
            var orderId = await _orderDao.SaveOrderAsync(order);

            // Assert
            Assert.That(orderId, Is.GreaterThan(0));

        }
    }
}
