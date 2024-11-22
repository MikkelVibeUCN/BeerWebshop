using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Test.APIClientLibraryTests
{
    [TestFixture]
    [NonParallelizable]
    public class OrderApiClientTests
    {
        private OrderApiClient _orderApiClient;
        private ProductAPIClient _productApiClient;
        private CategoryAPIClient _categoryApiClient;
        private BreweryAPIClient _breweryApiClient;
        private AccountAPIClient _accountAPIClient;

        private readonly string _baseUri = "https://localhost:7244/api/v1/";
        private readonly string _testSuffix = "_Test";
        private readonly List<int> _createdOrderIds = new();
        private readonly List<int> _createdProductIds = new();
        private readonly List<int> _createdCategoryIds = new();
        private readonly List<int> _createdBreweryIds = new();
        private readonly List<int> _createdCustomersIds = new();

        [SetUp]
        public async Task SetUpAsync()
        {
            _orderApiClient = new OrderApiClient(_baseUri);
            _productApiClient = new ProductAPIClient(_baseUri);
            _categoryApiClient = new CategoryAPIClient(_baseUri);
            _breweryApiClient = new BreweryAPIClient(_baseUri);
            _accountAPIClient = new AccountAPIClient(_baseUri);

            var categoryDto = new CategoryDTO { Name = $"IPA{_testSuffix}" };
            var categoryId = await _categoryApiClient.CreateAsync(categoryDto);
            _createdCategoryIds.Add(categoryId);

            var breweryDto = new BreweryDTO { Name = $"Overtone{_testSuffix}" };
            var breweryId = await _breweryApiClient.CreateAsync(breweryDto);
            _createdBreweryIds.Add(breweryId);

            var customerDto = new CustomerDTO
            {
                Name = "Thomas Lugter",
                Email = "Hej@ds.dk",
                Address = "Testvej 1 1234 Testby",
                Phone = "12345678"
            };
            var customerId = await _accountAPIClient.CreateAsync(customerDto);
            _createdCustomersIds.Add(customerId);

        }

        [TearDown]
        public async Task TearDownAsync()
        {
            foreach (var orderId in _createdOrderIds)
            {
                try
                {
                    await _orderApiClient.DeleteAsync(orderId);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Failed to delete order with ID {orderId}: {ex.Message}");
                }
            }

            foreach (var productId in _createdProductIds)
            {
                try
                {
                    await _productApiClient.DeleteAsync(productId);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Failed to delete product with ID {productId}: {ex.Message}");
                }
            }

            foreach (var breweryId in _createdBreweryIds)
            {
                try
                {
                    await _breweryApiClient.DeleteAsync(breweryId);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Failed to delete brewery with ID {breweryId}: {ex.Message}");
                }
            }

            foreach (var categoryId in _createdCategoryIds)
            {
                try
                {
                    await _categoryApiClient.DeleteAsync(categoryId);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Failed to delete category with ID {categoryId}: {ex.Message}");
                }
            }

            foreach (var customerId in _createdCustomersIds)
            {
                try
                {
                    await _accountAPIClient.DeleteAsync(customerId);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Failed to delete customer with ID {customerId}: {ex.Message}");
                }
            }

            _createdOrderIds.Clear();
            _createdProductIds.Clear();
            _createdCategoryIds.Clear();
            _createdBreweryIds.Clear();
        }

        [Test]
        public async Task SaveOrder_WhenCustomerIsNotLoggedIn_ShouldCreateOrder()
        {
            var productDto = CreateTestProductDto();
            var productId = await _productApiClient.CreateAsync(productDto);
            _createdProductIds.Add(productId);

            var retrievedProductDto = await _productApiClient.GetAsync(productId);
            Assert.IsNotNull(retrievedProductDto, "Product retrieval failed; product should not be null.");
            Assert.AreEqual(productDto.Name, retrievedProductDto.Name, "Product names should match.");

            var orderDto = new OrderDTO
            {
                Date = DateTime.Now,
                OrderLines = new List<OrderLineDTO>
                {
                    new OrderLineDTO
                    {
                        Product = retrievedProductDto,
                        Quantity = 2
                    }
                },
                CustomerDTO = await _accountAPIClient.GetByStringAsync("Hej@ds.dk"),
                IsDelivered = false
            };

            var orderId = await _orderApiClient.CreateAsync(orderDto);
            _createdOrderIds.Add(orderId);

            Assert.That(orderId, Is.GreaterThan(0), "Order ID should be greater than 0 for a valid order with null CustomerDTO.");
        }
        [Test]
        public async Task SaveOrder_WhenCustomerIsNull_ShouldThrowException()
        {
            var productDto = CreateTestProductDto();
            var productId = await _productApiClient.CreateAsync(productDto);
            _createdProductIds.Add(productId);

            var retrievedProductDto = await _productApiClient.GetAsync(productId);
            Assert.IsNotNull(retrievedProductDto, "Product retrieval failed; product should not be null.");
            Assert.AreEqual(productDto.Name, retrievedProductDto.Name, "Product names should match.");

            var orderDto = new OrderDTO
            {
                Date = DateTime.Now,
                OrderLines = new List<OrderLineDTO>
        {
            new OrderLineDTO
            {
                Product = retrievedProductDto,
                Quantity = 2
            }
        },
                CustomerDTO = null,
                IsDelivered = false
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await _orderApiClient.CreateAsync(orderDto));

            Assert.That(ex.Message, Does.Contain("Error creating OrderDTO"), "CustomerDTO is required.");
        }




        [Test]
        public async Task GetOrderFromId_WhenOrderExists_ShouldReturnOrder()
        {
            var productDto = CreateTestProductDto();
            var productId = await _productApiClient.CreateAsync(productDto);
            _createdProductIds.Add(productId);

            var retrievedProductDto = await _productApiClient.GetAsync(productId);

            var orderDto = new OrderDTO
            {
                Date = DateTime.Now,
                OrderLines = new List<OrderLineDTO>
                {
                    new OrderLineDTO
                    {
                        Product = retrievedProductDto,
                        Quantity = 2
                    }
                },
                CustomerDTO = await _accountAPIClient.GetByStringAsync("Hej@ds.dk"),
                IsDelivered = false
            };

            var orderId = await _orderApiClient.CreateAsync(orderDto);
            _createdOrderIds.Add(orderId);

            var fetchedOrderDto = await _orderApiClient.GetAsync(orderId);
            Assert.IsNotNull(fetchedOrderDto, $"Order with ID {orderId} should exist.");
            Assert.That(fetchedOrderDto.Id, Is.EqualTo(orderId), "Returned Order ID should match the created order ID.");
        }

        private ProductDTO CreateTestProductDto()
        {
            return new ProductDTO
            {
                Id = 0,
                Name = $"Integration Test Product{_testSuffix}",
                CategoryName = $"IPA{_testSuffix}",
                BreweryName = $"Overtone{_testSuffix}",
                Price = 10.0f,
                Description = $"Sample product for integration test{_testSuffix}",
                Stock = 20,
                ABV = 5.5f,
                ImageUrl = "http://example.com/image.jpg",
                RowVersion = ""
            };
        }
    }
}