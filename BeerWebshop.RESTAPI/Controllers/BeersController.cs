using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerDAO _beerDao;

        public BeersController(IBeerDAO beerDao)
        {
            _beerDao = beerDao;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody] Beer beer)
        {
            var result = _beerDao.CreateBeerAsync(beer);
            return Ok(result);
        }
    }
}
