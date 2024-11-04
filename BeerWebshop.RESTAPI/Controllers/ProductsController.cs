using BeerWebshop.DAL.DATA.DAO.DAOClasses;
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
            var result = _productDao.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            var result = _productDao.CreateAsync(product);
            return Ok(result);
        }
    }
}
