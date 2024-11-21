using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BeerWebshop.Web.Services
{
	public class AccountService
	{
		private readonly IAccountAPIClient _accountAPIClient;
		private readonly CookieService _cookieService;
		private readonly JWTService _jwtService;
		private const string AuthCookieKey = "AuthCookie";

		public AccountService(IAccountAPIClient accountAPIClient, CookieService cookieService, JWTService jwtService)
		{
			_accountAPIClient = accountAPIClient;
			_cookieService = cookieService;
            _jwtService = jwtService;
        }

		public async Task<CustomerDTO?> GetCustomerByEmailAsync(string email)
		{
			return await _accountAPIClient.GetByEmailAsync(email);
		}

		public async Task<int> CreateCustomerAsync(AccountCreationViewModel viewModel)
		{
			try
			{
				string address = $"{viewModel.Street} {viewModel.StreetNumber}";
				if (!string.IsNullOrEmpty(viewModel.ApartmentNumber))
				{
					address += $" {viewModel.ApartmentNumber}";
				}

				address += $" {viewModel.PostalCode} {viewModel.City}";

				CustomerDTO customer = new CustomerDTO
				{
					Name = $"{viewModel.FirstName} {viewModel.LastName}",
					Address = address,
					Email = viewModel.Email,
					Password = viewModel.Password,
					Phone = viewModel.Phone,
					Age = viewModel.Age
				};
				return await _accountAPIClient.CreateAsync(customer);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating customer: {ex.Message}", ex);
			}

		}

		public async Task<CustomerDTO?> GetCustomerFromLoginCookie()
		{
            string? token = GetTokenCookie();
            string? email = _jwtService.GetEmailFromToken(token);

            if (!string.IsNullOrEmpty(email))
            {
                CustomerDTO? customer = await GetCustomerByEmailAsync(email);
                if (customer != null)
                {
                    return customer;
                }
            }
            return null;
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
		public async Task<string?> AuthenticateAndGetTokenAsync(LoginViewModel loginViewModel)
		{
			var customerDTO = await _accountAPIClient.GetByEmailAsync(loginViewModel.Email);
			if (customerDTO != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, customerDTO.Password))
			{
				return _jwtService.GenerateJwtToken(customerDTO.Email);
            }
            return null;
		}

	}
}