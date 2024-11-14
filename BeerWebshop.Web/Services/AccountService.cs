using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
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

        // Retrieve hashed password from the cookie
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

        public void RemoveAuthCookie()
        {
            _cookieService.RemoveCookies<string>(AuthCookieKey);
        }
    }
}
