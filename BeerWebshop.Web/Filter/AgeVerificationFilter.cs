using BeerWebshop.Web.Controllers;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeerWebshop.Web.Filter
{
    public class AgeVerificationFilter : ActionFilterAttribute
    {
        private readonly AgeVerifierService _ageVerifierService;
        public AgeVerificationFilter(AgeVerifierService ageVerifierService)
        {
            _ageVerifierService = ageVerifierService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.Controller is HomeController) { return; }
            if (!_ageVerifierService.HasUserConfirmedAge())
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
