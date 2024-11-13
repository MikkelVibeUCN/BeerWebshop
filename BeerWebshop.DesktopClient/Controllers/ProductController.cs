using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DesktopClient.Controllers
{
    public class ProductController
    {
        private IProductAPIClient productAPIClient;
        public ProductController()
        {
            
        }
        public ProductController(ProductAPIClient productAPIClient)
        {
            this.productAPIClient = new ProductAPIClient("https://localhost:7244/api/v1/");
        }

        public async Task<int>AddProductAsync(ProductDTO product)
        {
            
            try
            {
                ProductDTO newProduct = new ProductDTO
                {
                    Name = product.Name,
                    BreweryName = product.BreweryName,
                    Price = product.Price,
                    Description = product.Description,
                    Stock = product.Stock,
                    ABV = product.ABV,
                    CategoryName = product.CategoryName,
                };

               return await productAPIClient.CreateProductAsync(newProduct);
            }
            catch (Exception ex)
            {
                throw new Exception($"Produkt ikke tilføjet: {ex.Message}");
            }
        }
    }
}
