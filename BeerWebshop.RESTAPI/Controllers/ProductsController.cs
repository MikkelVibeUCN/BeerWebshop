using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly BreweryService _breweryService;
        public ProductsController(ProductService productService, CategoryService categoryService, BreweryService breweryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _breweryService = breweryService;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Product with id {id} was not found.");
            }
            var Product = MapToDTO(result);
            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDTO Product)
        {
            if (Product == null)
            {
                return BadRequest("Product data is required.");
            }

            var product = await MapToEntity(Product);

            var productId = await _productService.CreateProductAsync(product);

            Product.Id = productId;

            return CreatedAtRoute("GetProductById", new { id = productId }, productId);
        }

        private ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id ?? 0,
                Name = product.Name,
                BreweryName = product.Brewery?.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                ABV = product.Abv,
                CategoryName = product.Category?.Name,
                ImageUrl = product.ImageUrl
            };
        }

        private async Task<Product> MapToEntity(ProductDTO product)
        {
            int? categoryId = await _categoryService.GetCategoryIdByName(product.CategoryName);
            if (categoryId == null) throw new Exception("Category not found");
            
            Category? category = await _categoryService.GetCategoryById((int)categoryId);
            if (category == null) throw new Exception("Category not found");

            int? breweryId = await _breweryService.GetBreweryIdByName(product.BreweryName);
            if (breweryId == null) throw new Exception("Brewery not found");

            Brewery? brewery = await _breweryService.GetBreweryById((int)breweryId);
            if (brewery == null) throw new Exception("Brewery not found");


            return new Product
            {
                Name = product.Name,
                Category = category,
                Brewery = brewery,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                Abv = product.ABV,
                ImageUrl = product.ImageUrl,
                IsDeleted = false
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromBody] ProductQueryParameters parameters)
        {
            try
            {
                var result = await _productService.GetProducts(parameters);

                List<ProductDTO> products = new List<ProductDTO>();
                foreach (var product in result)
                {
                    products.Add(MapToDTO(product));
                }

                return Ok(products);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetProductCountFromParameters([FromBody]ProductQueryParameters parameters)
        {
            try
            {
                var result = await _productService.GetProductsCount(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}