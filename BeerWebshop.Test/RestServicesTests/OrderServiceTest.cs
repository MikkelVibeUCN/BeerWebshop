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
		private Mock<IProductDAO> _productDaoMock;
		private Order _testOrder;
		private Product _testProduct;

		[SetUp]
		public void SetUp()
		{
			// mock-objekter til IOrderDAO og IProductDAO, som giver mulighed for at teste uden  database
			_orderDaoMock = new Mock<IOrderDAO>();
			_productDaoMock = new Mock<IProductDAO>();

			// Connection string er kun placeholder, vi bruger mocks
			var connectionString = Configuration.ConnectionString();
			_orderService = new OrderService(_orderDaoMock.Object, _productDaoMock.Object, connectionString);

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
				CustomerId_FK = 1,
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
			_productDaoMock.Setup(x => x.GetByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// Tester optimistic concurrency ved at sikre, at UpdateStockOptimisticAsync returnerer "true",
			// hvilket betyder, at stock blev opdateret uden konflikter.
			_productDaoMock.Setup(x => x.UpdateStockOptimisticAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion))
						   .ReturnsAsync(true);

			// Act 
			var orderId = await _orderService.CreateOrderAsync(_testOrder);

			// Assert 
			Assert.That(orderId, Is.EqualTo(expectedOrderId), "Det returnerede ordre ID bør matche det forventede.");
			_orderDaoMock.Verify(x => x.InsertCompleteOrderAsync(It.IsAny<Order>()), Times.Once, "InsertCompleteOrderAsync skal kaldes præcist én gang.");
			_productDaoMock.Verify(x => x.UpdateStockOptimisticAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion), Times.Once);
		}

		[Test]
		public void CreateOrderAsync_WhenProductStockIsInsufficient_ShouldThrowException()
		{
			// Arrange
			// Test, hvor produktets lagerbeholdning er lavere end det nødvendige
			_testProduct.Stock = 1; // For lav stock
			_productDaoMock.Setup(x => x.GetByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(
				() => _orderService.CreateOrderAsync(_testOrder),
				"En InvalidOperationException throwes, når produktets stock er for lavt."
			);

			// Verificer, at opdatering af lagerbeholdning ikke blev forsøgt
			_productDaoMock.Verify(x => x.UpdateStockOptimisticAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<byte[]>()), Times.Never);
		}

		[Test]
		public void CreateOrderAsync_WhenProductHasBeenModifiedByAnotherTransaction_ShouldThrowException()
		{
			// Arrange
			// Mock der indikerer at produktet blev ændret af en anden transaktion
			_productDaoMock.Setup(x => x.GetByIdAsync(_testProduct.Id ?? 0))
						   .ReturnsAsync(_testProduct);

			// UpdateStockOptimisticAsync returnerer false, hvilket betyder, at produktets stock blev ændret af en anden
			_productDaoMock.Setup(x => x.UpdateStockOptimisticAsync(_testProduct.Id ?? 0, 2, _testProduct.RowVersion))
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
			_productDaoMock.Reset();
		}
	}
}
