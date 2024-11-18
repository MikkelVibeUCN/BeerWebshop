using BeerWebshop.APIClientLibrary;
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
        private IProductAPIClient _productAPIClient;
        public ProductController(ProductAPIClient productAPIClient)
        {
            _productAPIClient = productAPIClient;

        }

        public async Task<int>AddProductAsync(ProductDTO product)
        {
            
            try
            {
                ProductDTO newProduct = new ProductDTO
                {
                    Id = 0,
                    Name = product.Name,
                    BreweryName = product.BreweryName,
                    Price = product.Price,
                    Description = product.Description,
                    Stock = product.Stock,
                    ABV = product.ABV,
                    CategoryName = product.CategoryName,
					RowVersion = "",
                    ImageUrl = ""
				};

               return await _productAPIClient.CreateAsync(newProduct);
            }
            catch (Exception ex)
            {
                throw new Exception($"Produkt ikke tilføjet: {ex.Message}");
            }
        }
        public async Task<IEnumerable<ProductDTO>> getProducts(ProductQueryParameters productQueryParameters) 
        {
            return await _productAPIClient.GetProductsAsync(productQueryParameters); 
        }

        public async Task<bool> DeleteProduct(ProductDTO product)
        {
            return await _productAPIClient.DeleteAsync(product.Id);
        }
        public async Task EditProduct(ProductDTO product)
        { 
            await _productAPIClient.EditProductAsync(product);
        }
    }
}
