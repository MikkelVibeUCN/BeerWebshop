using BeerWebshop.Web.Models;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;

        public CheckoutController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: CheckoutController
        public ActionResult Index()
        {
            var cart = _cartService.GetCart();
            var model = new Checkout
            {
                Cart = cart
            };
            return View(model);
        }

        // POST: CheckoutController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Checkout model)
        {
            if (ModelState.IsValid)
            {
                // Here you can process the order and save it to your database
                // After processing, you can redirect to a confirmation page
                return RedirectToAction("OrderConfirmation");
            }

            // If model state is not valid, return to the view with the same model
            model.Cart = _cartService.GetCart(); // Reload cart
            return View(model);
        }

        // GET: Order Confirmation
        public ActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
