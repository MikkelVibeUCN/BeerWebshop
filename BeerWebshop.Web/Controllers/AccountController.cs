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

            // Authenticate and get the hashed password if successful
            var hashedPassword = await _accountService.AuthenticateAndGetHashedPasswordAsync(loginDTO);
            if (hashedPassword != null)
            {
                _accountService.SetAuthCookie(hashedPassword, loginDTO.Email); // Store hashed password and email
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
