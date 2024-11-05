using BeerWebshop.Web.ApiClient;
using BeerWebshop.Web.ApiClient.DTO;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
    public class BeerController : Controller
    {
        private readonly BeerService _beerService;

        public BeerController(BeerService beerService)
        {
            _beerService = beerService;
        }

        // GET: BeerController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> beers = _beerService.GetTenLatestBeers();
            return View(beers);
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_beerService.GetBeerFromId(id));
        }


    }
}
