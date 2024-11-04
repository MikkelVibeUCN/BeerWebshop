using BeerWebshop.RESTAPI.DTO;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerDao _beerDao;

        public BeersController(IBeerDao beerDao)
        {
            _beerDao = beerDao;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody] BeerDTO beer)
        {
            var result = _beerDao.CreateBeerAsync(beer);
            return Ok(result);
        }
    }
}
