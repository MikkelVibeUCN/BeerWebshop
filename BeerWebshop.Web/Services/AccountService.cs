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


        public void RemoveAuthCookie()
    {
        _cookieService.RemoveCookies<string>(AuthCookieKey);
    }
    public string? GetHashedPasswordFromCookie()
    {
        var authDataJson = _cookieService.GetObjectFromCookie<string>(AuthCookieKey);
        if (authDataJson != null)
        {
            var authData = JsonConvert.DeserializeObject<dynamic>(authDataJson);
            return authData.HashedPassword;
        }
        return null;
    }
    // Set auth cookie with hashed password and email
    public void SetAuthCookie(string hashedPassword, string email)
    {
        var authData = new
        {
            Email = email,
            HashedPassword = hashedPassword
        };

        var authDataJson = JsonConvert.SerializeObject(authData);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        };

        _cookieService.SaveCookie(authDataJson, AuthCookieKey, cookieOptions);
    }
    // Authenticate user and return hashed password if successful
    public async Task<string?> AuthenticateAndGetHashedPasswordAsync(LoginDTO loginDTO)
    {
        var customerDTO = await _accountAPIClient.GetCustomerByEmail(loginDTO.Email);
        if (customerDTO != null && BCrypt.Net.BCrypt.Verify(loginDTO.Password, customerDTO.Password))
        {
            return customerDTO.Password; // Return the hashed password to store in the cookie
        }
        return null;
    }

}
}

                





            
        
