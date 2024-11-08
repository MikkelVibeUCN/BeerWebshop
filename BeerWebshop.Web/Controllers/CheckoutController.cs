using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BeerWebshop.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly OrderService _orderService;
        private readonly CheckoutService _checkoutService;

        public CheckoutController(ICartService cartService, CheckoutService checkoutService, OrderService orderService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _orderService = orderService;
        }

        // GET: CheckoutController
        public ActionResult Index()
        {
            ShoppingCart cart = _cartService.GetCart();
            if(cart.OrderLines.Count == 0)
            {
                return Redirect("/Cart");
            }

            CheckoutViewModel model = new CheckoutViewModel
            {
                Cart = cart,
                Checkout = _checkoutService.GetCheckout()
            };

            return View(model);
        }
        // POST: CheckoutController
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutViewModel model)
        {
            OrderDTO order = await _orderService.SaveOrder(model);

            return RedirectToAction("OrderConfirmation", "Checkout", order);
        }

        [HttpPost]
        public IActionResult SaveCheckout([FromBody] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                // Save the checkout information using your service
                _checkoutService.UpdateCheckout(checkout);

                return Ok(new { success = true });
            }
            return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // GET: OrderDTO Confirmation
        public ActionResult OrderConfirmation(OrderDTO orderDTO)
        {
            return View(orderDTO);
        }
    }
}
