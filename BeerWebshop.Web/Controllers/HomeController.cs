using BeerWebshop.Web.Models;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace BeerWebshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgeVerifierService _ageVerifierService;
        public HomeController(ILogger<HomeController> logger, AgeVerifierService ageVerifierService)
        {
            _logger = logger;
            _ageVerifierService = ageVerifierService;
        }

        public IActionResult Index()
        {
            ViewData["ShowAgeVerificationModal"] = !_ageVerifierService.IsUserAbove18();

            return View();
        }

        [HttpPost]
        public IActionResult ConfirmAge()
        {
            _ageVerifierService.SetUserAgeBool();

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
