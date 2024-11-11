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
	private BreweryDAO _breweryDao;
	private CategoryDAO _categoryDao;

	private int _createdProductId;
	private int _createdBreweryId;
	private int _createdCategoryId;
	private int _testCustomerId = 1;

	// SetUp-metode, som kører før hver test for at initialisere nødvendige objekter og data
	[SetUp]
	public async Task SetUpAsync()
	{
		var connectionString = Configuration.ConnectionString();
		_orderDao = new OrderDAO(connectionString);
		_productDao = new ProductDAO(connectionString);
		_breweryDao = new BreweryDAO(connectionString);
		_categoryDao = new CategoryDAO(connectionString);

		// Test brewery
		var brewery = new Brewery
		{
			Name = "Morsleutel",
			IsDeleted = false,
		};
		_createdBreweryId = await _breweryDao.CreateBreweryAsync(brewery);
		brewery.Id = _createdBreweryId;

		// Test category
		var category = new Category
		{
			Name = "Tipa",
			IsDeleted = false,
		};
		_createdCategoryId = await _categoryDao.CreateCategoryAsync(category);
		category.Id = _createdCategoryId;

		// Test produkt, hvor Id bruges i orderlines
		var product = new Product
		{
			Name = "Tuborg pilsner",
			Category = category,
			Brewery = brewery,
			Price = 5.99f,
			Description = "En forfriskende pilsner med et strejf af citrus.",
			Stock = 10,
			Abv = 4.5f,
			ImageUrl = "https://example.com/images/lager_delight.jpg",
			IsDeleted = false,
		};
		_createdProductId = await _productDao.CreateAsync(product);
	}

	// TearDown-metode, som kører efter hver test for at slette testdata fra databasen
	[TearDown]
	public async Task TearDown()
	{
		// Sletter først alle orderlines, der refererer til produktet, før vi forsøger at slette produktet
		await DeleteOrderLinesByProductId(_createdProductId);

		if (_createdProductId != 0)
		{
			await _productDao.DeleteAsync(_createdProductId);
		}
		if (_createdCategoryId != 0)
		{
			await _categoryDao.DeleteAsync(_createdCategoryId);
		}
		if (_createdBreweryId != 0)
		{
			await _breweryDao.DeleteAsync(_createdBreweryId);
		}
	}

	[Test]
	public async Task InsertCompleteOrderAsync_WhenCalled_ShouldInsertOrderAndOrderLinesWithCorrectPrice()
	{

		var product = await _productDao.GetByIdAsync(_createdProductId);

		var quantity = 2;
		var expectedTotal = quantity * product!.Price;

		// OrderLine, der indeholder referencen til produkt og antal
		var orderLine = new OrderLine
		{
			ProductId = _createdProductId,
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
		using var connection = new SqlConnection(Configuration.ConnectionString());
		var orderLineData = await connection.QuerySingleAsync<(int Count, float SumTotal)>(
			"SELECT COUNT(1) AS Count, SUM(Total) AS SumTotal FROM OrderLines WHERE OrderId = @OrderId AND ProductId = @ProductId",
			new { OrderId = orderId, ProductId = _createdProductId });

		Assert.That(orderLineData.Count, Is.EqualTo(1), "Der bør være præcis én orderline med det specificerede OrderId og ProductId.");
		Assert.That(orderLineData.SumTotal, Is.EqualTo(expectedTotal).Within(0.01), "Orderline totalen bør matche den forventede beregnede pris.");

		// Oprydning
		await DeleteOrderById(orderId);
	}

	//Helper methods

	private async Task DeleteOrderLinesByProductId(int productId)
	{
		using var connection = new SqlConnection(Configuration.ConnectionString());
		await connection.ExecuteAsync("DELETE FROM OrderLines WHERE ProductId = @ProductId", new { ProductId = productId });
	}

	private async Task DeleteOrderById(int orderId)
	{
		await DeleteOrderLinesByOrderId(orderId);

		using var connection = new SqlConnection(Configuration.ConnectionString());
		await connection.ExecuteAsync("DELETE FROM Orders WHERE Id = @OrderId", new { OrderId = orderId });
	}

	private async Task DeleteOrderLinesByOrderId(int orderId)
	{
		using var connection = new SqlConnection(Configuration.ConnectionString());
		await connection.ExecuteAsync("DELETE FROM OrderLines WHERE OrderId = @OrderId", new { OrderId = orderId });
	}
}
