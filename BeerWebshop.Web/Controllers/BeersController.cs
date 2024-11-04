using BeerWebshop.Web.ApiClient;
using BeerWebshop.Web.ApiClient.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
    public class BeersController : Controller
    {
        private readonly IRestClient _restClient;

        public BeersController(IRestClient restClient)
        {
            _restClient = restClient;
        }

        // GET: BeerController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Beer> beers = _restClient.GetTenLatestBeers();
            return View(beers);
        }

    }
}
