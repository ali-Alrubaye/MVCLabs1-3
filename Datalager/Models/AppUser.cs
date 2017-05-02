using Microsoft.AspNet.Identity.EntityFramework;

namespace Datalager.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsAdministrator { get; set; }
    }
}