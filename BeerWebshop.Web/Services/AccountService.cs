using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace BeerWebshop.Web.Services
{
	public class AccountService
	{
		private readonly IAccountAPIClient _accountAPIClient;
		private readonly CookieService _cookieService;
		private const string AuthCookieKey = "AuthCookie";

		public AccountService(IAccountAPIClient accountAPIClient, CookieService cookieService)
		{
			_accountAPIClient = accountAPIClient;
			_cookieService = cookieService;
        }

        public async Task<string?> GetLoginToken(LoginViewModel loginViewModel)
        {
			return await _accountAPIClient.GetLoginToken(loginViewModel);
        }

		public async Task<int?> CreateCustomerAsync(CustomerDTO customer)
		{
			return await _accountAPIClient.CreateAsync(customer);
        }

        public async Task<string?> CreateCustomerAsync(AccountCreationViewModel viewModel)
		{
			return await _accountAPIClient.CreateAsync(viewModel);
        }

        public async Task<CustomerDTO?> GetCustomerFromLoginCookie()
		{
            string? token = GetTokenCookie();
			if(string.IsNullOrEmpty(token))
			{
				return null;
			}
			return await GetLoggedInCustomer(token);
        } 

		public string? GetTokenCookie()
		{
            return _cookieService.GetObjectFromCookie<string>(AuthCookieKey);
        }

		public void RemoveTokenCookie()
		{
			_cookieService.RemoveCookies<string>(AuthCookieKey);
		}
		public void SaveTokenCookie(string token)
		{
			CookieOptions options = new CookieOptions
            {
                Expires = System.DateTime.Now.AddMinutes(60),
                HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Lax
            };
            _cookieService.SaveCookie<string>(token, AuthCookieKey, options);
		}

        public async Task<int?> GetCustomerIdFromToken()
        {
            CustomerDTO? customer = await GetCustomerFromLoginCookie();
            return customer != null ? customer.Id : null;
        }
		
		public async Task<CustomerDTO?> GetLoggedInCustomer(string token)
		{
			return await _accountAPIClient.GetAsync(token);
        }

		public async Task<string?> AuthorizeLogin(LoginViewModel loginView)
		{
			return await _accountAPIClient.GetLoginToken(loginView);
        }
	}
}