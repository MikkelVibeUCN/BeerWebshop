using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly AccountService _accountService;

		public AccountController(AccountService accountService)
		{
			_accountService = accountService;
		}

        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            bool isAuthenticated = await _accountService.AuthenticateUserAsync(loginDTO);
            if (isAuthenticated)
            {
                _accountService.SetAuthCookie("user_token_or_id"); // Replace with actual token or user ID
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(loginDTO);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _accountService.RemoveAuthCookie();
            return RedirectToAction("Index", "Home");
        }
    }
}
