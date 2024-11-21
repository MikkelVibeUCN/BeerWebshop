using BeerWebshop.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace BeerWebshop.Web.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly object _lock = new object(); // Synchronization lock

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T? GetObjectFromCookie<T>(string cookieKey)
        {
            // Locking ensures only one thread can access this block at a time
            lock (_lock)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new Exception("HttpContext was null");
                }

                var cookieValue = httpContext.Request.Cookies[cookieKey];

                if (cookieValue == null)
                {
                    return default;
                }

                var objectT = JsonConvert.DeserializeObject<T>(cookieValue);
                if (objectT == null)
                {
                    throw new Exception("Failed to deserialize cart from cookies");
                }
                return objectT;
            }
        }

        public void RemoveCookies<T>(string cookieKey)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext was null");
            }

            httpContext.Response.Cookies.Delete(cookieKey);
        }

        public void SaveCookie<T>(T objectToSaveToCookie, string cookieKey, CookieOptions? options = null)
        {
            var cookieOptions = options ?? new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext was null");
            }

            var cartJson = JsonConvert.SerializeObject(objectToSaveToCookie);
            httpContext.Response.Cookies.Append(cookieKey, cartJson, cookieOptions);
        }
    }
}
