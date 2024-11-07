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

    [Test]
    public async Task GetProductsFromId_WhenProductExist_ShouldReturnProduct()
    {
        int existingBeerId = 27;

        var result = await _productAPIClient.GetProductFromIdAsync(existingBeerId);

        Assert.NotNull(result);
        Assert.AreEqual(existingBeerId, result.Id);
    }

    [Test]
    public async Task GetProductCategories_WhenCategoriesExist_ShouldReturnAllCategories()
    {
        var categories = (await _productAPIClient.GetProductCategoriesAsync()).ToList();

        Assert.AreEqual(3, categories.Count);
        Assert.IsTrue(categories.Contains("IPA"));
        Assert.IsTrue(categories.Contains("Lager"));
    }

    [Test]
    public async Task CreateProductAsync_WhenProductIsValid_ShouldReturnNewProductId()
    {
        var newProduct = new Product
        {
            Name = "PISOGSPYT",
            Brewery = "Brewery A",
            Price = 15.99f,
            Description = "Urinær.",
            Stock = 100,
            ABV = 5.5f,
            Type = "IPA",
            ImageUrl = "http://example.com/test-image.jpg"
        };

        //[Test]
        //public async Task GetBeerFromCategoryAsync_WhenCategoryExist_ShouldReturnBeers()
        //{
        //    var productsFromCategory = (await _productAPIClient.GetBeerByCategory("IPA")).ToList();

        //    Assert.IsTrue(productsFromCategory.Count >= 5, "There should be at least five product in the IPA category.");
        //    Assert.IsTrue(productsFromCategory.All(p => p.Category == "IPA"), "All products should be in the IPA category.");
        //}

        //[Test]
        //public async Task GetAllBeersAsync_WhenBeersExist_ShouldReturnAllBeers()
        //{
        //    var allProducts = (await _productAPIClient.GetAllBeersAsync()).ToList();

        //    Assert.IsTrue(allProducts.Count >= 5, "There should be at least ten products in the database.");
        //    Assert.IsTrue(allProducts.All(p => p.Id > 0), "All products should have an ID greater than 0.");
        //}

    }
}
