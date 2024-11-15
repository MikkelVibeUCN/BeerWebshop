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

        public IActionResult Login()
        {
            return View(new LoginDTO());
        }
        [HttpGet]
        public IActionResult CreateAccount() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO); 
            }

            try
            {
                var customer = await _accountService.GetCustomerByEmailAsync(loginDTO.Email);
                if (customer == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(loginDTO);
                }

                var hashedPassword = await _accountService.AuthenticateAndGetHashedPasswordAsync(loginDTO);
                if (hashedPassword != null)
                {
                    _accountService.SetAuthCookie(hashedPassword, loginDTO.Email, (int)customer.Id);
                    return RedirectToAction("Index", "Home"); 
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return View(loginDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");

                ModelState.AddModelError("", "An error occurred while attempting to log in. Please try again.");
                return View(loginDTO);
            }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            _accountService.RemoveAuthCookie();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountCreationViewModel viewModel)
        {
            try
            {
                await _accountService.CreateCustomerAsync(viewModel);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the account.");
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> AccountOverview()
        {
            var customerId = _accountService.GetCustomerIdFromCookie();
            
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);

			return View(orders);
		}

        
	}
}
