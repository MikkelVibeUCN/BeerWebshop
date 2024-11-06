using BeerWebshop.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace BeerWebshop.Web.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartCookieKey = "Cart";

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ShoppingCart GetCartFromCookies()
        {
            var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return new ShoppingCart();
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(cookieValue);
        }

        public void SaveCartToCookies(ShoppingCart cart)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), 
                HttpOnly = true,
                SameSite = SameSiteMode.Lax 
            };

            var cartJson = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CartCookieKey, cartJson, cookieOptions);
        }

        public void RemoveCartFromCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(CartCookieKey);
        }
    }
}
