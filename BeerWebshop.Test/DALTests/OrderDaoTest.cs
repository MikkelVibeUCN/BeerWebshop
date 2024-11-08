using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.Test.DALTests
{
	public class OrderDaoTest
	{
		public OrderDao _orderDao;
		public ProductDAO _productDao;
		public BreweryDAO _breweryDao;
		public CategoryDAO _categoryDao;
		private int _createdProductId;
		private int _createdCategoryId;
		private int _createdBreweryId;
		private int _createdOrderLine;
		private Order _order;
		private Product _product;

		[SetUp]
		public async Task SetUpAsync()
		{
			_orderDao = new OrderDao(Configuration.ConnectionString());
			_productDao = new ProductDAO(Configuration.ConnectionString());
			_breweryDao = new BreweryDAO(Configuration.ConnectionString());
			_categoryDao = new CategoryDAO(Configuration.ConnectionString());

			_order = new Order(DateTime.Now, new List<OrderLine>(), "123 Main St", false, 1);

			var brewery = new Brewery()
			{
				Name = "Morsleutel",
				IsDeleted = false,
			};
			_createdBreweryId = await _breweryDao.CreateBreweryAsync(brewery);

			var category = new Category()
			{
				Name = "Tipa",
				IsDeleted = false,
			};
			_createdCategoryId = await _categoryDao.CreateCategoryAsync(category);

			var product = new Product()
			{
				Name = "Tuborg pilsner",
				CategoryId_FK = _createdCategoryId,
				BreweryId_FK = _createdBreweryId,
				Price = 5.99f,
				Description = "A refreshing lager with a hint of citrus.",
				Stock = 10,
				Abv = 4.5f,
				ImageUrl = "https://example.com/images/lager_delight.jpg",
				IsDeleted = false,
			};
			_createdProductId = await _productDao.CreateAsync(product);

			var orderLine = new OrderLine()
			{
				ProductId = _createdProductId,
				Quantity = 1
			};
			_order.OrderLines.Add(orderLine);
		}

		[Test]
		public async Task InsertOrderAsync_WhenOrderIsInserted_ShouldReturnOrderId()
		{
			var orderId = await _orderDao.SaveOrderAsync(_order);

			Assert.That(orderId, Is.GreaterThan(0));
		}
	}
}
