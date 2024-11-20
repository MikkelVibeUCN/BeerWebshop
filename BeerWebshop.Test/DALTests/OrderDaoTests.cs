using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class OrderDaoTests
{
	private OrderDAO _orderDao;
	private ProductDAO _productDao;
	private BreweryDAO _breweryDao;
	private CategoryDAO _categoryDao;
	private AccountDAO _accountDao;

	private string _testSuffix = "_Test";

	[SetUp]
	public void SetUp()
	{
		var connectionString = DBConnection.ConnectionString();

		_accountDao = new AccountDAO(connectionString);
		_orderDao = new OrderDAO(connectionString);
		_productDao = new ProductDAO(connectionString);
		_breweryDao = new BreweryDAO(connectionString);
		_categoryDao = new CategoryDAO(connectionString);
	}

	[Test]
	public async Task InsertCompleteOrderAsync_WhenCalled_ShouldInsertOrderAndOrderLinesWithCorrectPrice()
	{
		Customer customer = new Customer
		{
			Name = $"Test{_testSuffix} Test{_testSuffix}",
			Phone = "12345678",
			Password = "password",
			Age = 20,
			Email = $"dsajkdkjjhad@dsasdaad",
			Address = "Street number 9000 aalborg"
		};
        //var customerId = await 
        var categoryId = await _categoryDao.CreateAsync(new Category { Name = $"Category{_testSuffix}", IsDeleted = false });
		var breweryId = await _breweryDao.CreateAsync(new Brewery { Name = $"Brewery{_testSuffix}", IsDeleted = false });
		var productId = await _productDao.CreateAsync(new Product
		{
			Name = $"Product{_testSuffix}",
			Category = new Category { Id = categoryId },
			Brewery = new Brewery { Id = breweryId },
			Price = 50f,
			Description = "Test product description.",
			Stock = 5,
			Abv = 5.0f,
			ImageUrl = "http://example.com/test.jpg",
			IsDeleted = false
		});

		var product = await _productDao.GetByIdAsync(productId);

		var quantity = 2;
		var expectedTotal = quantity * product!.Price;

		var orderLine = new OrderLine
		{
			Quantity = quantity,
			Product = product
		};

		var order = new Order
		{
			CreatedAt = DateTime.Now,
			DeliveryAddress = "Smith Residence",
			IsDelivered = false,
			Customer = customer,
			IsDeleted = false,
			OrderLines = new List<OrderLine> { orderLine }
		};

		var orderId = await _orderDao.CreateAsync(order);

		Assert.That(orderId, Is.GreaterThan(0), "Order ID should be a positive integer.");

		using var connection = new SqlConnection(DBConnection.ConnectionString());
		var orderLineData = await connection.QuerySingleAsync<(int Count, float SumTotal)>(
			"SELECT COUNT(1) AS Count, SUM(Total) AS SumTotal FROM OrderLines WHERE OrderId = @OrderId AND ProductId = @ProductId",
			new { OrderId = orderId, ProductId = productId });

		Assert.That(orderLineData.Count, Is.EqualTo(1), "There should be exactly one order line with the specified OrderId and ProductId.");
		Assert.That(orderLineData.SumTotal, Is.EqualTo(expectedTotal).Within(0.01), "The order line total should match the expected calculated price.");

		await _orderDao.DeleteAsync(orderId);
		await _productDao.DeleteAsync(productId);
		await _breweryDao.DeleteAsync(breweryId);
		await _categoryDao.DeleteAsync(categoryId);
	}
	//Kan ikke teste på at der ikke er nok stock i DAO, da checket sker i OrderService

}
