using BeerWebshop.Web.ApiClient;
using BeerWebshop.Web.ApiClient.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
    public class BeerController : Controller
    {
        private readonly IRestClient _restClient;

        public BeerController(IRestClient restClient)
        {
            _restClient = restClient;
        }

        // GET: BeerController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Beer> beers = _restClient.GetTenLatestBeers();
            return View(beers);
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_restClient.GetBeerFromId(id));
        }


    }
}
