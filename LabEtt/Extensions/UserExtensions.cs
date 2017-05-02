using System.Security.Claims;
using System.Security.Principal;

namespace LabEtt.Extensions
{
    public static class UserExtensions
    {
        public static bool IsAdministrator(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;

            return claimsIdentity != null && claimsIdentity.HasClaim("IsAdministrator", "True");
        }
    }
}