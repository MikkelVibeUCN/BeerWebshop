using BeerWebshop.APIClientLibrary;
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
        public async Task<IActionResult> Index(ProductQueryParameters parameters)
        {
            ViewBag.Categories = await _beerService.GetBeerCategories();

            ViewBag.CurrentSortOrder = parameters.SortBy;
            ViewBag.CurrentCategory = parameters.Category;

            IEnumerable<Product> beers = await _beerService.GetBeers(parameters);

            return View(beers);
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_beerService.GetBeerFromId(id));
        }


    }
}
