using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Test.APIClientLibraryTests;

public class ProductApiClientTest
{
    private ProductAPIClient _productAPIClient;

    [SetUp]
    public async Task SetUp()
    {
        _productAPIClient = new ProductAPIClient("https://localhost:7244/api/v1/");
    }
    //TODO: We need to fix this test so it isnt hardcoded
    [Test]
    public async Task GetProductsFromId_WhenProductExist_ShouldReturnProduct()
    {
        int existingBeerId = 69;

        var result = await _productAPIClient.GetProductFromIdAsync(existingBeerId);

        Assert.NotNull(result);
        Assert.AreEqual(existingBeerId, result.Id);
    }

    
}
