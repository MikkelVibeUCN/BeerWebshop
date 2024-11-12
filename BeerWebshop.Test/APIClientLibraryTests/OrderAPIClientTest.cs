using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Test.APIClientLibraryTests;

[TestFixture]
public class OrderApiClientTests
{
	private OrderApiClient _orderApiClient;
	private ProductAPIClient _productApiClient;

	private string _baseUri = "https://localhost:7244/api/v1/";
	private int _createdOrderId;
	private int _createdProductId;

	[SetUp]
	public void SetUp()
	{
		_orderApiClient = new OrderApiClient(_baseUri);
		_productApiClient = new ProductAPIClient(_baseUri);
		_createdOrderId = 0;

	}

	[Test]
	public async Task SaveOrder_WhenOrderHasNullCustomer_ShouldReturnOrderId()
	{
		var productDto = new ProductDTO
		{
			Name = "Integration Test Product",
			CategoryName = "IPA",
			BreweryName = "Overtone",
			Price = 10.0f,
			Description = "Sample product for integration test",
			Stock = 20,
			ABV = 5.5f,
			ImageUrl = "http://example.com/image.jpg"
		};

		_createdProductId = await _productApiClient.CreateProductAsync(productDto);

		var retrievedProductDto = await _productApiClient.GetProductFromIdAsync(_createdProductId);
		Assert.IsNotNull(retrievedProductDto, "Product retrieval failed; product should not be null.");
		Assert.AreEqual(productDto.Name, retrievedProductDto.Name, "Product names should match.");

		OrderDTO orderDto = new OrderDTO(DateTime.Now, new List<OrderLineDTO>(), null, false);

		var orderline = new OrderLineDTO
		{

			Product = retrievedProductDto,
			Quantity = 2
		};

		orderDto.OrderLines.Add(orderline);

		_createdOrderId = await _orderApiClient.SaveOrder(orderDto);

		Assert.That(_createdOrderId, Is.GreaterThan(0), "Order ID should be greater than 0 for a valid order with null CustomerDTO.");

	}

	[Test]
	public async Task GetOrderFromId_WhenOrderExists_ShouldReturnOrder()
	{
        var productDto = new ProductDTO
        {
            Name = "Integration Test Product",
            CategoryName = "IPA",
            BreweryName = "Overtone",
            Price = 10.0f,
            Description = "Sample product for integration test",
            Stock = 20,
            ABV = 5.5f,
            ImageUrl = "http://example.com/image.jpg"
        };

        _createdProductId = await _productApiClient.CreateProductAsync(productDto);

        var retrievedProductDto = await _productApiClient.GetProductFromIdAsync(_createdProductId);

        OrderDTO orderDto = new OrderDTO(DateTime.Now, new List<OrderLineDTO>(), null, false);

        var orderline = new OrderLineDTO
        {

            Product = retrievedProductDto,
            Quantity = 2
        };

        orderDto.OrderLines.Add(orderline);

        _createdOrderId = await _orderApiClient.SaveOrder(orderDto);

		orderDto = await _orderApiClient.GetOrderFromId(_createdOrderId);

		Assert.IsNotNull(orderDto, $"Order with ID {_createdOrderId} should exist.");
		Assert.That(orderDto.Id, Is.EqualTo(_createdOrderId), "Returned Order ID should match the created order ID.");
	}

	[TearDown]
	public async Task TearDown()
	{
		if (_createdOrderId > 0)
		{
			await _orderApiClient.DeleteOrder(_createdOrderId);
			await _productApiClient.DeleteProductByIdAsync(_createdProductId);
		}
	}
}
