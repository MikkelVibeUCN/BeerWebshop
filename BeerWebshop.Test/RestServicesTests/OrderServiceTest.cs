using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;

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


		[SetUp]
		public async Task SetUp()
		{
			var productDao = new ProductDAO(_connectionString);
			var orderDao = new OrderDAO(_connectionString);
			var categoryDao = new CategoryDAO(_connectionString);
			var breweryDao = new BreweryDAO(_connectionString);

			var categoryService = new CategoryService(categoryDao);
			var breweryService = new BreweryService(breweryDao);

			_productService = new ProductService(productDao, categoryService, breweryService);

			_orderService = new OrderService(orderDao, _productService, _connectionString);

			var testBrewery = await breweryService.GetBreweryById(15);
			var testCategory = await categoryService.GetCategoryById(15);

			var testProductDTO = new ProductDTO
			{
				Name = "Sample Product",
				BreweryName = testBrewery.Name,
				Price = 10.0f,
				Description = "Sample Description",
				Stock = 10,
				ABV = 5.0f,
				CategoryName = testCategory.Name,
				ImageUrl = "https://example.com/sample-image.jpg",
				RowVersion = ""

			};

			int productId = await _productService.CreateProductAsync(testProductDTO);

			_testProduct = await productDao.GetByIdAsync(productId);

			_testOrder = new Order
			{
				CreatedAt = DateTime.Now,
				DeliveryAddress = "123 Test Ave",
				IsDelivered = false,
				OrderLines = new List<OrderLine>
		{
			new OrderLine
			{
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


		[TearDown]
		public async Task TearDown()
		{
			if (_createdOrderId > 0)
			{
				await _orderService.DeleteOrderByIdAsync(_createdOrderId);
				_createdOrderId = 0;
			}
		}
	}
}
