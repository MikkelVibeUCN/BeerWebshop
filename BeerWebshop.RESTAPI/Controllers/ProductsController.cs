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

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody] Product beer)
        {
            var result = _productDAO.CreateBeerAsync(beer);
            return Ok(result);
        }
    }
}
