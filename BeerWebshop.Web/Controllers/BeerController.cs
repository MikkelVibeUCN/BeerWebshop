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
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_restClient.GetBeerFromId(id));
        }

        // GET: BeerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Beer beer)
        {
            try
            {
                _restClient.AddNewBeer(beer);           return Redirect("/home/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BeerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BeerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
