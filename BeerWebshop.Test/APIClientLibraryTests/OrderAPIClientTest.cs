using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.Test.APIClientLibraryTests;

[TestFixture]
public class OrderApiClientTests
{
	private OrderApiClient _orderApiClient;
	private ProductAPIClient _productApiClient;

	private string _baseUri = "https://localhost:7244/api/v1/";
	private int _createdOrderId;

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
		int productId = 299;
		var product = await _productApiClient.GetProductFromIdAsync(productId);

		Assert.IsNotNull(product);

		var orderDto = new OrderDTO
		{
			Date = DateTime.Now,
			IsDelivered = false,
			CustomerDTO = null,
			OrderLines = new List<OrderLineDTO>
				{
					new OrderLineDTO
					{
						Product = product,
						Quantity = 2
					}
				}
		};

		_createdOrderId = await _orderApiClient.SaveOrder(orderDto);

		Assert.That(_createdOrderId, Is.GreaterThan(0), "Order ID should be greater than 0 for a valid order with null CustomerDTO.");
	}


	[Test]
	public async Task GetOrderFromId_WhenOrderExists_ShouldReturnOrder()
	{
		Assume.That(_createdOrderId, Is.GreaterThan(0), "A valid order ID must be created in the SaveOrder test.");

		var orderDto = await _orderApiClient.GetOrderFromId(_createdOrderId);

		Assert.IsNotNull(orderDto, $"Order with ID {_createdOrderId} should exist.");
		Assert.That(orderDto.Id, Is.EqualTo(_createdOrderId), "Returned Order ID should match the created order ID.");
	}

	[TearDown]
	public async Task TearDown()
	{
		if (_createdOrderId > 0)
		{
			await _orderApiClient.DeleteOrder(_createdOrderId);
		}
	}
}
