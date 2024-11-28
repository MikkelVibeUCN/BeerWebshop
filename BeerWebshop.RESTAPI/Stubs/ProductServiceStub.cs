using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Stubs
{
    public class ProductServiceStub
    {
        public async Task<Product?> GetProductByIdAsyncStub(int id)
        {
            return await Task.FromResult<Product?>(new Product(
       id: 1,
       name: "Test Beer",
       category: new Category { Id = 1, Name = "IPA" }, // Simulated Category
       brewery: new Brewery { Id = 1, Name = "Test Brewery" }, // Simulated Brewery
       price: 12.99f,
       description: "A refreshing IPA with citrus and hop notes.",
       stock: 50,
       abv: 5.5f,
       imageUrl: "http://example.com/images/test-beer.jpg",
       isDeleted: false
   ));
        }
    }
}
