using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;

namespace BeerWebshop.RESTAPI.Stubs
{
    public class ProductServiceStub : IProductService
    {
        private List<Product> _products = new List<Product>();


        public ProductServiceStub() { }
        public Task<int> CreateProductAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = new ProductDTO()
            {
                Id = 1,
                Name = "test",
                BreweryName = "test",
                Price = 100,
                Description = "test",
                Stock = 10,
                ABV = 10,
                CategoryName = "test",
                ImageUrl = "pornhub.com",
                RowVersion = string.Empty,
            };

            return Task.FromResult(product);
    
    
        }



        public Task<Product?> GetProductEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetProductsCount(ProductQueryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }
    }
}
