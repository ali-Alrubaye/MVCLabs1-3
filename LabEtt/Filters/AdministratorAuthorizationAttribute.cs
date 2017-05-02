using System.Security.Claims;
using System.Web.Mvc;

namespace LabEtt.Filters
{
    public class AdministratorAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var principal = filterContext.HttpContext.User
                as ClaimsPrincipal;

            if (principal == null || !principal.HasClaim("IsAdministrator", "True"))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}