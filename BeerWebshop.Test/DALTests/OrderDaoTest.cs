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
    //public class OrderDaoTest
    //{
    //    public OrderDao _orderDao;
        
    //    [SetUp]
    //    public async Task SetUpAsync()
    //    {
    //        _orderDao = new OrderDao(Configuration.ConnectionString());
           
    //    }
    //    [Test]
    //    public async Task InsertOrderAsync_WhenOrderIsInserted_ShouldReturnOrderId()
    //    {
    //        // Arrange
    //        var order = new Order(DateTime.Now, new List<OrderLine>(), "123 Main St", false);
    //        var product = new Product
    //        {
    //            Id = 1,
    //            Name = "Test Beer",
    //            Brewery = "Test Brewery",
    //            Price = 5.99f,
    //            Description = "A test beer for unit testing.",
    //            Stock = 10,
    //            ABV = 4.5f,
    //            Category = "Test Category"
    //        };
    //        var orderLine1 = new OrderLine(2, product);
    //        order.AddOrderLine(orderLine1);
    //        // Act
    //        var orderId = await _orderDao.SaveOrderAsync(order);

    //        // Assert
    //        Assert.That(orderId, Is.EqualTo(1));
            
    //    }
    //}
}
