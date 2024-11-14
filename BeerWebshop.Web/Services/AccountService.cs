using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Services
{
	public class AccountService
	{
		private readonly IAccountAPIClient _accountAPIClient;
		private readonly CookieService _cookieService;
		private const string AuthCookie = "AuthCookie";

        public AccountService(IAccountAPIClient accountAPIClient, CookieService cookieService)
        {
            _accountAPIClient = accountAPIClient;
			_cookieService = cookieService;
		}

		public async Task<bool> AuthenticateUserAsync(LoginDTO loginDTO)
		{
			var customerDTO = await _accountAPIClient.GetCustomerByEmail(loginDTO.Email);
            return customerDTO.Password != null && BCrypt.Net.BCrypt.Verify(loginDTO.Password, customerDTO.Password);
		}

		public void SetAuthCookie(string authToken)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddHours(1)
			};

			_cookieService.SaveCookie(authToken, AuthCookie, cookieOptions);
		}

		public string? GetAuthTokenFromCookie()
        {
            return _cookieService.GetObjectFromCookie<string>(AuthCookie);
        }

        public void RemoveAuthCookie()
		{
			_cookieService.RemoveCookies<string>("AuthCookie");
		}

	}
}
