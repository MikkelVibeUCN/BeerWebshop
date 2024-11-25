using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Cookies;
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
        private readonly AccountService _accountService;
        public CheckoutController(ICartService cartService, CheckoutService checkoutService, OrderService orderService, AccountService accountService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _orderService = orderService;
            _accountService = accountService;
        }

        // GET: CheckoutController
        public async Task<ActionResult> Index([FromBody] CheckoutViewModel? viewModel)
        {
            if(viewModel == null)
            {
                viewModel = new CheckoutViewModel
                {
                    Cart = _cartService.GetCart(),
                    Checkout = _checkoutService.GetCheckout(),
                    Customer = await _accountService.GetCustomerFromLoginCookie()
                };
            }
            if (viewModel.Cart.OrderLines.Count == 0)
            {
                return Redirect("/Cart");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] Checkout checkout)
        {
            CheckoutViewModel viewModel = new CheckoutViewModel
            {
                Checkout = _checkoutService.GetCheckout(),
                Cart = _cartService.GetCart(),
                Customer = await _accountService.GetCustomerFromLoginCookie()
            };

            if (viewModel.Customer == null)
            {
                // Attach errors to the Checkout object within the viewModel
                if (string.IsNullOrWhiteSpace(checkout.Firstname))
                {
                    ModelState.AddModelError($"Checkout.{nameof(checkout.Firstname)}", "Fornavn mangler.");
                }
                if (string.IsNullOrWhiteSpace(checkout.Lastname))
                {
                    ModelState.AddModelError($"Checkout.{nameof(checkout.Lastname)}", "Efternavn mangler.");
                }
                if (string.IsNullOrWhiteSpace(checkout.Phonenumber))
                {
                    ModelState.AddModelError($"Checkout.{nameof(checkout.Phonenumber)}", "Telefonnummer mangler.");
                }
                if (string.IsNullOrWhiteSpace(checkout.Email))
                {
                    ModelState.AddModelError($"Checkout.{nameof(checkout.Email)}", "Email mangler.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                if(viewModel.Customer == null)
                {
                    viewModel.Customer = new CustomerDTO
                    {
                        Name = $"{checkout.Firstname} {checkout.Lastname}",
                        Phone = checkout.Phonenumber,
                        Email = checkout.Email,
                        Address = $"{checkout.Street} {checkout.Number} {checkout.PostalCode} {checkout.City}"
                    };
                    viewModel.Customer.Id = await _accountService.CreateCustomerAsync(viewModel.Customer);
                }

                int id = await _orderService.SaveOrder(viewModel);

                _cartService.ClearCartCookies();

                return RedirectToAction("OrderConfirmation", "Checkout", new { orderId = id });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public IActionResult SaveCheckout([FromBody] Checkout checkout)
        {
            _checkoutService.UpdateCheckout(checkout);
            return Ok(new { success = true });
        }

        // GET: Order Confirmation
        public async Task<ActionResult> OrderConfirmation(int orderId)
        {
            string? token = _accountService.GetTokenCookie();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Account");
            }

            OrderDTO? order = await _orderService.GetOrderFromId(orderId, token);
            if (order == null)
            {
                return BadRequest("Order not found");
            }
            return View(order);
        }
    }
}
