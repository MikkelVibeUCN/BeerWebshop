﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
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

        public async Task<ActionResult> Index()
        {
            if (await _accountService.GetCustomerIdFromCookie() != null)
            {
                return RedirectToAction("AccountOverview", "Account");
            }
            return View(new LoginViewModel());
        }

        public IActionResult CreateAccount() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel); 
            }

            try
            {
                var customer = await _accountService.GetCustomerByEmailAsync(loginViewModel.Email);
                if (customer == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(loginViewModel);
                }

                var hashedPassword = await _accountService.AuthenticateAndGetHashedPasswordAsync(loginViewModel);
                if (hashedPassword != null)
                {
                    AuthCookie newAuthCookie = new AuthCookie
                    {
                        Email = loginViewModel.Email,
                        PasswordHash = hashedPassword
                    };
                    _accountService.SaveAuthCookie(newAuthCookie);
                    return RedirectToAction("Index", "Account"); 
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");

                ModelState.AddModelError("", "An error occurred while attempting to log in. Please try again.");
                return View(loginViewModel);
            }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            _accountService.RemoveAuthCookie();
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
                await _accountService.CreateCustomerAsync(viewModel);

                _accountService.SaveAuthCookie(new AuthCookie
                {
                    Email = viewModel.Email,
                    PasswordHash = await _accountService.AuthenticateAndGetHashedPasswordAsync(new LoginViewModel
                    {
                        Email = viewModel.Email,
                        Password = viewModel.Password
                    })
                });

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
            int? customerId = await _accountService.GetCustomerIdFromCookie();

            if (customerId == null)
            {
                return RedirectToAction("Index", "Account");
            }
            var orders = await _orderService.GetOrdersByCustomerIdAsync((int)customerId);
			return View(orders);
		}

        
	}
}
