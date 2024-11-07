using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDAO _productDao;

        public ProductsController(IProductDAO productDAO)
        {
            _productDao = productDAO;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productDao.GetByIdAsync(id);
            var Product = MapToDTO(result);
            if (result == null)
            {
                return NotFound($"Product with id {id} was not found.");
            }
            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product Product)
        {
            if (Product == null)
            {
                return BadRequest("Product data is required.");
            }

            var product = await MapToEntity(Product);

            var productId = await _productDao.CreateAsync(product);

            Product.Id = productId;

            return CreatedAtRoute("GetProductById", new { id = productId }, productId);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _productDao.GetProductCategoriesAsync();
            return Ok(result);
        }


        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetFromCategoryAsync(string category)
        {
            var result = await _productDao.GetFromCategoryAsync(category);
            return Ok(result);
        }

        private Product MapToDTO(Product product)
        {
            return new Product
            {
                Id = product.Id ?? 0, 
                Name = product.Name,
                Brewery = product.Brewery?.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                ABV = product.Abv,
                Type = product.Category?.Name,
                ImageUrl = product.ImageUrl
            };
        }

        private async Task<Product> MapToEntity(Product Product)
        {
            return new Product
            {
                Name = Product.Name,
                CategoryId_FK = await _productDao.GetCategoryIdByName(Product.Type), 
                BreweryId_FK = await _productDao.GetBreweryIdByName(Product.Brewery), 
                Price = Product.Price,
                Description = Product.Description,
                Stock = Product.Stock,
                Abv = Product.ABV,
                ImageUrl = Product.ImageUrl,
                IsDeleted = false 
            };
        }
    }
}
