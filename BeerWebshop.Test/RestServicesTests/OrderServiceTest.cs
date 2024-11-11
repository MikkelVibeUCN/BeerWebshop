using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.Test.DALTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWebshop.Test.RestServicesTests
{
	[TestFixture]
	public class OrderServiceTests
	{
		private OrderService _orderService;
		private ProductService _productService;
		private Order _testOrder;
		private Product _testProduct;
		private string _connectionString = DBConnection.ConnectionString();
		private int _createdOrderId;
		private int _createdProductId;

		[SetUp]
		public async Task SetUp()
		{
			var productDao = new ProductDAO(_connectionString);
			var orderDao = new OrderDAO(_connectionString);

			_productService = new ProductService(productDao);
			_orderService = new OrderService(orderDao, _productService, _connectionString);

			_testProduct = new Product
			{
				Name = "Sample Product",
				Description = "Sample Description",
				Abv = 5.0f,
				Price = 10,
				Stock = 10,
				IsDeleted = false,
				Category = new Category { Id = 268, Name = "IPA", IsDeleted = false },
				Brewery = new Brewery { Id = 262, Name = "Overtone", IsDeleted = false },
				ImageUrl = "https://example.com/sample-image.jpg"
			};

			_createdProductId = await _productService.CreateProductAsync(_testProduct);
			_testProduct = await _productService.GetProductByIdAsync(_createdProductId);
			_testProduct.Id = _createdProductId;

			_testOrder = new Order
			{
				Date = DateTime.Now,
				DeliveryAddress = "123 Test Ave",
				IsDelivered = false,
				OrderLines = new List<OrderLine>
		{
			new OrderLine
			{
				ProductId = _testProduct.Id ?? 0,
				Quantity = 2,
				Product = _testProduct
			}
		}
			};
		}

		[Test]
		public async Task CreateOrderAsync_WhenOrderIsValid_ShouldReturnOrderId()
		{
			_createdOrderId = await _orderService.CreateOrderAsync(_testOrder);

			Assert.That(_createdOrderId, Is.GreaterThan(0), "The returned order ID should be greater than 0.");

			var updatedProduct = await _productService.GetProductByIdAsync(_testProduct.Id ?? 0);
			Assert.That(updatedProduct.Stock, Is.EqualTo(_testProduct.Stock - 2), "The product stock should be reduced by the order quantity.");
		}

		[Test]
		public async Task UpdateStock_WithConcurrentModification_ShouldThrowException()
		{
			var productForFirstUpdate = await _productService.GetProductByIdAsync(_testProduct.Id ?? 0);
			var productForSecondUpdate = await _productService.GetProductByIdAsync(_testProduct.Id ?? 0);

			bool firstUpdateResult = await _productService.UpdateStockAsync(
				productForFirstUpdate.Id ?? 0, 5, productForFirstUpdate.RowVersion);

			Assert.IsTrue(firstUpdateResult, "The first update should succeed.");

			var updatedProduct = await _productService.GetProductByIdAsync(_testProduct.Id ?? 0);
			Assert.That(updatedProduct.Stock, Is.EqualTo(5), "Stock should be reduced to 5 after the first update.");

			var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await _productService.UpdateStockAsync(productForSecondUpdate.Id ?? 0, 5, productForSecondUpdate.RowVersion);
			});

			Assert.That(ex, Is.TypeOf<InvalidOperationException>());
		}



		[TearDown]
		public async Task TearDown()
		{
			if (_createdOrderId > 0)
			{
				await _orderService.DeleteOrderByIdAsync(_createdOrderId);
				_createdOrderId = 0;
			}

			if (_createdProductId > 0)
			{
				await _productService.DeleteProductByIdAsync(_createdProductId);
			}
		}
	}
}
