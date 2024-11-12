using BeerWebshop.Web.Models;

namespace BeerWebshop.Web.Services
{
    public class AgeVerifierService
    {
        private readonly string _ageCookieKey = "AgeCookie";
        private readonly CookieService _cookieService;

        public AgeVerifierService(CookieService cookieService)
        {
            this._cookieService = cookieService;
        }

        public AgeCookie GetAgeCookie()
        {
            AgeCookie? ageCookie = _cookieService.GetObjectFromCookie<AgeCookie>(_ageCookieKey);

            if(ageCookie == null)
            {
                ageCookie = new AgeCookie()
                {
                    IsOver18 = false
                };
                SaveCookie(ageCookie);
            }
            return ageCookie;
        }

        public bool IsUserAbove18()
        {
            AgeCookie ageCookie = GetAgeCookie();
            return ageCookie.IsOver18;
        }

        public void SaveCookie(AgeCookie ageCookie)
        {
            _cookieService.SaveCookie<AgeCookie>(ageCookie, _ageCookieKey);
        }

        public void SetUserAgeBool()
        {
            AgeCookie ageCookie = GetAgeCookie();

            ageCookie.IsOver18 = true;

            SaveCookie(ageCookie);
        }
    }
}
