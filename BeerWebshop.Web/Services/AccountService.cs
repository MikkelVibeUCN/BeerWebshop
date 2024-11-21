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
		private const string AuthCookieKey = "AuthCookie";

		public AccountService(IAccountAPIClient accountAPIClient, CookieService cookieService)
		{
			_accountAPIClient = accountAPIClient;
			_cookieService = cookieService;
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
					Id = 0,
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
			AuthCookie authCookie = GetAuthCookie();
			if(!string.IsNullOrEmpty(authCookie.Email))
			{
				return await GetCustomerByEmailAsync(authCookie.Email);
            }
			return null;
		} 

		public AuthCookie GetAuthCookie()
		{
			AuthCookie? authCookie = _cookieService.GetObjectFromCookie<AuthCookie>(AuthCookieKey);
            if (authCookie == null)
			{
				authCookie = new()
				{
					PasswordHash = "",
					Email = "",
				};
				SaveAuthCookie(authCookie);
            }
            return authCookie;
        }

		public void RemoveAuthCookie()
		{
			_cookieService.RemoveCookies<AuthCookie>(AuthCookieKey);
		}
		public string? GetHashedPasswordFromCookie()
		{
			return GetAuthCookie().PasswordHash;
        }
		// Set auth cookie with hashed password and email
		public void SaveAuthCookie(AuthCookie authCookie)
		{
			_cookieService.SaveCookie<AuthCookie>(authCookie, AuthCookieKey);
		}

		public async Task<int?> GetCustomerIdFromCookie()
		{
			AuthCookie authCookie = GetAuthCookie();
			if (!string.IsNullOrEmpty(authCookie.Email)) 
			{
				CustomerDTO? customer = await GetCustomerByEmailAsync(authCookie.Email);
				if (customer != null)
				{
					return customer.Id;
				}
			}
			return null;	
		}
		// Authenticate user and return hashed password if successful
		public async Task<string?> AuthenticateAndGetHashedPasswordAsync(LoginViewModel loginViewModel)
		{
			var customerDTO = await _accountAPIClient.GetByEmailAsync(loginViewModel.Email);
			if (customerDTO != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, customerDTO.Password))
			{
				return customerDTO.Password; // Return the hashed password to store in the cookie
			}
			return null;
		}

	}
}









