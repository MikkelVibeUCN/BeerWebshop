﻿using BeerWebshop.APIClientLibrary.ApiClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public async Task GetBeerByIdAsyncTest_WhenBeerExist_ShouldReturnBeer()
    {
        int existingBeerId = 1;
        
        var result = await _productAPIClient.GetBeerByIdAsync(existingBeerId);

        Assert.NotNull(result);
        Assert.AreEqual(existingBeerId, result.Id);
    }

    [Test]
    public async Task GetBeerFromCategoryAsync_WhenCategoryExist_ShouldReturnBeers()
    {
        var productsFromCategory = (await _productAPIClient.GetBeerByCategory("IPA")).ToList();

        Assert.IsTrue(productsFromCategory.Count >= 5, "There should be at least five product in the IPA category.");
        Assert.IsTrue(productsFromCategory.All(p => p.Category == "IPA"), "All products should be in the IPA category.");
    }

    [Test]
    public async Task GetAllBeersAsync_WhenBeersExist_ShouldReturnAllBeers()
    {
        var allProducts = (await _productAPIClient.GetAllBeersAsync()).ToList();

        Assert.IsTrue(allProducts.Count >= 5, "There should be at least ten products in the database.");
        Assert.IsTrue(allProducts.All(p => p.Id > 0), "All products should have an ID greater than 0.");
    }

}
