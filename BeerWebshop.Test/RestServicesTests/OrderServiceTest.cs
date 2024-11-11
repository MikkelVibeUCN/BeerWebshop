using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.Test.DALTests;
using Moq;

namespace BeerWebshop.Test.RestServicesTests
{
	[TestFixture]
	public class OrderServiceTests
	{
		private OrderService _orderService;
		private Mock<IOrderDAO> _orderDaoMock;
		private Mock<ProductService> _productServiceMock;
		private Order _testOrder;
		private Product _testProduct;

		[SetUp]
		public void SetUp()
		{
			_orderDaoMock = new Mock<IOrderDAO>();
			_productServiceMock = new Mock<ProductService>(Mock.Of<IProductDAO>()); // Use mock for ProductService

			// Placeholder connection string for testing
			_orderService = new OrderService(_orderDaoMock.Object, _productServiceMock.Object, string.Empty);

			// Test produk
			_testProduct = new Product
			{
				Id = 1,
				Name = "Sample Product",
				Price = 10.0f,
				Stock = 10,
				RowVersion = new byte[] { 1, 2, 3, 4 },
				IsDeleted = false
			};

			// Test order med test produktet
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
			// Arrange
			// Forventet ordre ID, når ordren er blevet indsat
			int expectedOrderId = 1;
			_orderDaoMock.Setup(x => x.InsertCompleteOrderAsync(It.IsAny<Order>()))
						 .ReturnsAsync(expectedOrderId);

			// Mock, der simulerer at hente et produkt fra databasen
			_productServiceMock.Setup(x => x.GetProductByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// Tester optimistic concurrency ved at sikre, at UpdateStockOptimisticAsync returnerer "true",
			// hvilket betyder, at stock blev opdateret uden konflikter.
			_productServiceMock.Setup(x => x.UpdateStockAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion))
						   .ReturnsAsync(true);

			// Act 
			var orderId = await _orderService.CreateOrderAsync(_testOrder);

			// Assert 
			Assert.That(orderId, Is.EqualTo(expectedOrderId), "Det returnerede ordre ID bør matche det forventede.");
			_orderDaoMock.Verify(x => x.InsertCompleteOrderAsync(It.IsAny<Order>()), Times.Once, "InsertCompleteOrderAsync skal kaldes præcist én gang.");
			_productServiceMock.Verify(x => x.UpdateStockAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion), Times.Once);
		}

		[Test]
		public void CreateOrderAsync_WhenProductStockIsInsufficient_ShouldThrowException()
		{
			// Arrange
			// Test, hvor produktets lagerbeholdning er lavere end det nødvendige
			_testProduct.Stock = 1; // For lav stock
			_productServiceMock.Setup(x => x.GetProductByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(
				() => _orderService.CreateOrderAsync(_testOrder),
				"En InvalidOperationException throwes, når produktets stock er for lavt."
			);

			// Verificer, at opdatering af lagerbeholdning ikke blev forsøgt
			_productServiceMock.Verify(x => x.UpdateStockAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<byte[]>()), Times.Never);
		}

		[Test]
		public void CreateOrderAsync_WhenProductHasBeenModifiedByAnotherTransaction_ShouldThrowException()
		{
			// Arrange
			// Mock der indikerer at produktet blev ændret af en anden transaktion
			_productServiceMock.Setup(x => x.GetProductByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// UpdateStockOptimisticAsync returnerer false, hvilket betyder, at produktets stock blev ændret af en anden
			_productServiceMock.Setup(x => x.UpdateStockAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion))
						   .ReturnsAsync(false);

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(
				() => _orderService.CreateOrderAsync(_testOrder),
				"En InvalidOperationException bør kastes, når produktet er blevet ændret af en anden transaktion."
			);
		}

		[Test]
		public async Task GetOrderByIdAsync_WhenOrderExists_ShouldReturnOrder()
		{
			// Arrange
			var orderId = 1;
			_testOrder.Id = orderId;
			_orderDaoMock.Setup(x => x.GetByIdAsync(orderId))
						 .ReturnsAsync(_testOrder);

			// Act 
			var result = await _orderService.GetOrderByIdAsync(orderId);

			// Assert 
			Assert.IsNotNull(result, "Den returnerede ordre bør ikke være null.");
			Assert.That(result.Id, Is.EqualTo(_testOrder.Id), "Ordre ID burde stemme overens.");
			Assert.That(result.DeliveryAddress, Is.EqualTo(_testOrder.DeliveryAddress), "Leveringsadressen burde stemme overens.");
			Assert.That(result.OrderLines.Count, Is.EqualTo(_testOrder.OrderLines.Count), "Antallet af orderlines burde stemme overens.");
		}

		// TearDown-metode, som nulstiller mocks efter hver test
		[TearDown]
		public void TearDown()
		{
			_orderDaoMock.Reset();
			_productServiceMock.Reset();
		}
	}
}
