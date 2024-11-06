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
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext was null");
            }

            var cookieValue = httpContext.Request.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return new ShoppingCart();
            }

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cookieValue);
            if (cart == null)
            {
                throw new Exception("Failed to deserialize cart from cookies");
            }
            return cart;
        }

        public void SaveCartToCookies(ShoppingCart cart)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext was null");
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            };

            var cartJson = JsonConvert.SerializeObject(cart);
            httpContext.Response.Cookies.Append(CartCookieKey, cartJson, cookieOptions);
        }

        public void RemoveCartFromCookies()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext was null");
            }

            httpContext.Response.Cookies.Delete(CartCookieKey);
        }
    }
}
