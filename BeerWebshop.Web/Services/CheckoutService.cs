using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class CheckoutService
    {
        private const string CheckoutCookieKey = "Checkout";
        private readonly CookieService _cookieService;

        public CheckoutService(CookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public Checkout GetCheckout()
        {
            Checkout? checkout = _cookieService.GetObjectFromCookie<Checkout>(CheckoutCookieKey);
            if (checkout == null)
            {
                checkout = new Checkout();
                SaveCheckout(checkout);
            }

            return checkout;
        }

        public void SaveCheckout(Checkout checkout)
        {
            _cookieService.SaveCookie(checkout, CheckoutCookieKey);
        }

        public void DeleteCheckout(Checkout checkout) 
        {
            _cookieService.RemoveCookies<Checkout>(CheckoutCookieKey);
        }

        public void UpdateCheckout(Checkout checkout)
        {
            SaveCheckout(checkout);
        }

    }
}
