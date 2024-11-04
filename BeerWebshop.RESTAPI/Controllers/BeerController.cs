using BeerWebshop.RESTAPI.DTO;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerDao _beerDao;

        public BeerController(IBeerDao beerDao)
        {
            _beerDao = beerDao;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody] BeerDTO beer)
        {
            var id = await _beerDao.CreateBeer(beer);
            return CreatedAtAction(nameof(GetBeer), new { id = id }, beer);
        }
    }
}
