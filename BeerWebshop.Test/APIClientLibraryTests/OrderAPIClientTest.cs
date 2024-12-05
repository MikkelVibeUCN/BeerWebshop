using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Test.APIClientLibraryTests;

[TestFixture]
public class OrderApiClientTests
{
    private OrderApiClient _orderApiClient;
    private ProductAPIClient _productApiClient;
    private AccountAPIClient _accountApiClient;
    private CategoryAPIClient _categoryApiClient;
    private BreweryAPIClient _breweryApiClient;
    private LoginViewModel _viewModel;
    private string _token;
    private readonly string _testSuffix = $"_Test_{new Guid()}";


    private string _baseUri = "https://localhost:7244/api/v1/";
    private readonly List<int> _createdProductIds = new();
    private readonly List<int> _createdCategoryIds = new();
    private readonly List<int> _createdBreweryIds = new();

    [SetUp]
    public async Task SetUp()
    {
        string apiUri = "https://localhost:7244/api/v1/";
        _orderApiClient = new OrderApiClient(_baseUri);
        _productApiClient = new ProductAPIClient(_baseUri);
        _accountApiClient = new AccountAPIClient(_baseUri);
        _categoryApiClient = new CategoryAPIClient(apiUri);
        _breweryApiClient = new BreweryAPIClient(apiUri);
        _viewModel = new LoginViewModel()
        {
            Email = "admin@admin",
            Password = "Password!123"
        };
        _token = await _accountApiClient.GetLoginToken(_viewModel);

        var categoryDto = new CategoryDTO { Name = $"IPA{_testSuffix}" };
        _createdCategoryIds.Add(await _categoryApiClient.CreateAsync(categoryDto, null, _token));

        var breweryDto = new BreweryDTO { Name = $"Overtone{_testSuffix}" };
        _createdBreweryIds.Add(await _breweryApiClient.CreateAsync(breweryDto, null, _token));
    }
    [TearDown]
    public async Task TearDown()
    {
        foreach (var productId in _createdProductIds)
        {
            try
            {
                await _productApiClient.DeleteAsync(productId, null, _token);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to delete product with ID {productId}: {ex.Message}");
            }
        }

        // Delete breweries
        foreach (var breweryId in _createdBreweryIds)
        {
            try
            {
                await _breweryApiClient.DeleteAsync(breweryId, null, _token);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to delete brewery with ID {breweryId}: {ex.Message}");
            }
        }

        // Delete categories
        foreach (var categoryId in _createdCategoryIds)
        {
            try
            {
                await _categoryApiClient.DeleteAsync(categoryId, null, _token);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to delete category with ID {categoryId}: {ex.Message}");
            }
        }

        _createdProductIds.Clear();
        _createdCategoryIds.Clear();
        _createdBreweryIds.Clear();
    }


    [Test]
    public async Task SaveOrder_WhenOrderHasNullCustomer_ShouldReturnOrderId()
    {

        var productDto = new ProductDTO
        {
            Name = "Integration Test Product",
            CategoryName = $"IPA{_testSuffix}",
            BreweryName = $"Overtone{_testSuffix}",
            Price = 10.0f,
            Description = "Sample product for integration test",
            Stock = 20,
            ABV = 5.5f,
            ImageUrl = "http://example.com/image.jpg",
            RowVersion = Convert.ToBase64String(new byte[8])
        };

        var productId = await _productApiClient.CreateAsync(productDto,null,_token);
        _createdProductIds.Add(productId);
        var retrievedProductDto = await _productApiClient.GetAsync(productId);
        Assert.IsNotNull(retrievedProductDto, "Product retrieval failed; product should not be null.");
        Assert.AreEqual(productDto.Name, retrievedProductDto.Name, "Product names should match.");

        OrderDTO orderDto = new OrderDTO(DateTime.Now, new List<OrderLineDTO>(), null, false);

        var orderline = new OrderLineDTO
        {

            Product = retrievedProductDto,
            Quantity = 2
        };

        orderDto.OrderLines.Add(orderline);

        var orderId = await _orderApiClient.CreateAsync(orderDto,null,_token);
        

        Assert.That(orderId, Is.GreaterThan(0), "Order ID should be greater than 0 for a valid order with null CustomerDTO.");

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
            ImageUrl = "http://example.com/image.jpg",
            RowVersion = Convert.ToBase64String(new byte[8])
        };

        var productId = await _productApiClient.CreateAsync(productDto, null, _token);

        var retrievedProductDto = await _productApiClient.GetAsync(productId);

        var orderDto = new OrderDTO
        {
            Date = DateTime.Now,
            OrderLines = new List<OrderLineDTO>(),
            CustomerDTO = null,
            IsDelivered = false

        };

        var orderline = new OrderLineDTO
        {
            Product = retrievedProductDto,
            Quantity = 2
        };

        orderDto.OrderLines.Add(orderline);

        var orderId = await _orderApiClient.CreateAsync(orderDto,null,_token);


        orderDto = await _orderApiClient.GetAsync(orderId);

        var fetchedOrderDto = await _orderApiClient.GetAsync(orderId);
        Assert.IsNotNull(fetchedOrderDto, $"Order with ID {orderId} should exist.");
        Assert.That(fetchedOrderDto.Id, Is.EqualTo(orderId), "Returned Order ID should match the created order ID.");
    }
}
