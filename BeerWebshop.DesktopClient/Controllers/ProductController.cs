using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.DesktopClient.Controllers
{
    public class ProductController
    {
        private IProductAPIClient _productAPIClient;
        public string JwtToken { get; private set; }
        public ProductController(ProductAPIClient productAPIClient, string? jwtToken = null)
        {
            _productAPIClient = productAPIClient;
            JwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));

        }

        public async Task<int> AddProductAsync(ProductDTO product)
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

                return await _productAPIClient.CreateAsync(newProduct, "Products", JwtToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Produkt ikke tilføjet: {ex.Message}");
            }
        }
        public async Task<IEnumerable<ProductDTO>> getProducts(ProductQueryParameters productQueryParameters)
        {
            return await _productAPIClient.GetProductsAsync(productQueryParameters, JwtToken);
        }

        public async Task<bool> DeleteProduct(ProductDTO product)
        {
            return await _productAPIClient.DeleteAsync(product.Id, JwtToken);
        }
        public async Task EditProduct(ProductDTO product)
        {
            await _productAPIClient.EditProductAsync(product, JwtToken);
        }
    }
}
