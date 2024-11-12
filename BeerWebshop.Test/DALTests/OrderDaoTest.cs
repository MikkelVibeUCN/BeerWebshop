using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class OrderDaoTest
{
	private OrderDAO _orderDao;
	private ProductDAO _productDao;

	private int _productId;
	private int _createdBreweryId;
	private int _createdCategoryId;
	private int _testCustomerId = 1;

	// SetUp-metode, som kører før hver test for at initialisere nødvendige objekter og data
	[SetUp]
	public async Task SetUpAsync()
	{
		var connectionString = DBConnection.ConnectionString();
		_orderDao = new OrderDAO(connectionString);
		_productDao = new ProductDAO(connectionString);



		_createdBreweryId = 537;
		_createdCategoryId = 548;
		_productId = 528;
	}

	// TearDown-metode, som kører efter hver test for at slette testdata fra databasen
	[TearDown]
	public async Task TearDown()
	{

	}

	[Test]
	public async Task InsertCompleteOrderAsync_WhenCalled_ShouldInsertOrderAndOrderLinesWithCorrectPrice()
	{

		var product = await _productDao.GetByIdAsync(_productId);

		var quantity = 2;
		var expectedTotal = quantity * product!.Price;

		// OrderLine, der indeholder referencen til produkt og antal
		var orderLine = new OrderLine
		{
			Quantity = quantity,
			Product = product
		};

		// Opretter  ordre med den tidligere definerede orderline
		var order = new Order
		{
			Date = DateTime.Now,
			DeliveryAddress = "Smith Residence",
			IsDelivered = false,
			CustomerId_FK = _testCustomerId,
			IsDeleted = false,
			OrderLines = new List<OrderLine> { orderLine }
		};

		// Indsætter ordren i databasen 
		var orderId = await _orderDao.InsertCompleteOrderAsync(order);

		Assert.That(orderId, Is.GreaterThan(0), "Ordre-ID'et bør være et positivt heltal.");

		// Kontrollerer at orderline blev indsat korrekt og har den forventede total
		using var connection = new SqlConnection(DBConnection.ConnectionString());
		var orderLineData = await connection.QuerySingleAsync<(int Count, float SumTotal)>(
			"SELECT COUNT(1) AS Count, SUM(Total) AS SumTotal FROM OrderLines WHERE OrderId = @OrderId AND ProductId = @ProductId",
			new { OrderId = orderId, ProductId = _productId });

		Assert.That(orderLineData.Count, Is.EqualTo(1), "Der bør være præcis én orderline med det specificerede OrderId og ProductId.");
		Assert.That(orderLineData.SumTotal, Is.EqualTo(expectedTotal).Within(0.01), "Orderline totalen bør matche den forventede beregnede pris.");

		// Oprydning
		await DeleteOrderById(orderId);
	}

	//Helper methods

	private async Task DeleteOrderLinesByProductId(int productId)
	{
		using var connection = new SqlConnection(DBConnection.ConnectionString());
		await connection.ExecuteAsync("DELETE FROM OrderLines WHERE ProductId = @ProductId", new { ProductId = productId });
	}

	private async Task DeleteOrderById(int orderId)
	{

		using var connection = new SqlConnection(DBConnection.ConnectionString());
		await connection.ExecuteAsync("DELETE FROM Orders WHERE Id = @OrderId", new { OrderId = orderId });
	}

}
