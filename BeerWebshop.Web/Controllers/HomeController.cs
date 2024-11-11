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
            ViewData["ShowAgeVerificationModal"] = !_ageVerifierService.HasUserConfirmedAge();

            if (!_ageVerifierService.IsUserAbove18() && _ageVerifierService.HasUserConfirmedAge())
            {
                return RedirectToPage("www.google.com");
            }

            return View();
        }

        [HttpPost]
        public IActionResult ConfirmedAge()
        {
            _ageVerifierService.SetUserHasConfirmedAgeBool(true);
            _ageVerifierService.SetUserAgeBool(true);

            return Ok();
        }

        [HttpPost] 
        public IActionResult DenyAge()
        {
            _ageVerifierService.SetUserHasConfirmedAgeBool(true);
            _ageVerifierService.SetUserAgeBool(false);

            return Ok();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
