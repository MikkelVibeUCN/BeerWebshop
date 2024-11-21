using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly OrderService _orderService;

		public AccountController(AccountService accountService, OrderService orderService)
        {
            _accountService = accountService;
			_orderService = orderService;
		}

        public async Task<ActionResult> Index(LoginViewModel? login)
        {
            if (await _accountService.GetCustomerFromLoginCookie() != null)
            {
                return RedirectToAction("AccountOverview", "Account");
            }
            _accountService.RemoveTokenCookie();

            if(login == null) { login = new LoginViewModel(); }

            return View(login);
        }

        public IActionResult CreateAccount() => View();


		[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errorMessage = "Ugyldige input. Tjek venligst dine oplysninger og prøv igen." });
            }

            try
            {
                var token = await _accountService.AuthenticateAndGetTokenAsync(loginViewModel);
                if (!string.IsNullOrEmpty(token))
                {
                    _accountService.SaveTokenCookie(token);

                    // Return the redirect URL as part of the JSON response
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Account") });
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return Json(new { success = false, errorMessage = "Invalid email or password." });
            }
            catch
            {
                return Json(new { success = false, errorMessage = "Forkert email eller adgangskode" });
            }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            _accountService.RemoveTokenCookie();
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountCreationViewModel viewModel)
        {
            if (viewModel.Age < 18)
            {
                return Json(new { success = false, errorMessage = "Du skal være mindst 18 år gammel for at oprette en konto." });
            }

            try
            {
                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password
                };

                await _accountService.CreateCustomerAsync(viewModel);

                string? token = await _accountService.AuthenticateAndGetTokenAsync(loginViewModel);

                if (string.IsNullOrEmpty(token))
                {
                    return Json(new { success = false, errorMessage = "Kunne ikke bekfæfte token" });
                }

                _accountService.SaveTokenCookie(token);

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Account") });
            }
            catch (Exception ex)
            {
                string message = "Der skete en fejl under oprettelsen af din konto. Prøv venligst igen.";
                if (ex.Message.ToLower().Contains("email"))
                {
                    message = "Den indtastede email er allerede i brug.";
                }
                return Json(new { success = false, errorMessage = message});
            }
        }

        public async Task<IActionResult> AccountOverview()
        {
            int? customerId = await _accountService.GetCustomerIdFromToken();

            if (customerId == null)
            {
                return RedirectToAction("Index", "Account");
            }
            var orders = await _orderService.GetOrdersByCustomerIdAsync((int)customerId);
			return View(orders);
		}

        
	}
}
