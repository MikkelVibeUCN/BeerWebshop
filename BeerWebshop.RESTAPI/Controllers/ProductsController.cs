using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
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
            var result = await _productDao.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            var productId = await _productDao.CreateAsync(product);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = productId }, product);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetFromCategoryAsync(string category)
        {
            var result = await _productDao.GetFromCategoryAsync(category);
            return Ok(result);
        }
    }
}
