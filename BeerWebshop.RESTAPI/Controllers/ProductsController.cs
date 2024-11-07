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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productDao.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} was not found.");
            }

            var productDTO = MapToDTO(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest("Product data is required");
            }
            var productId = await _productDao.CreateAsync(product);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = productId }, product);
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

        private ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
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
    }
}
