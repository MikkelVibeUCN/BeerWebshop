﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
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

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateAccount() => View();


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
    }
}
