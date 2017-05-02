using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Datalager;
using Microsoft.AspNet.Identity.EntityFramework;
using Datalager.Models;
using Microsoft.AspNet.Identity;

namespace LabEtt.Controllers
{
    public class AuthenticationController : Controller
    {
        private UserManager<AppUser> userManager;
        public AuthenticationController()
        {
            var context = new IdentityContext();
            var store = new UserStore<AppUser>(context);

            userManager = new UserManager<AppUser>(store);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string username,
            string password,
            string email)
        {
            var user = new AppUser
            {
                UserName = username,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var identity = await userManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                identity.AddClaim(new Claim("IsAdministrator",
                    user.IsAdministrator.ToString()));

                identity.AddClaim(new Claim("Email", user.Email));

                var authenticationManager =
                    HttpContext.GetOwinContext().Authentication;

                authenticationManager.SignIn(identity);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(
            string username,
            string password)
        {

            var user = await userManager.FindAsync(username, password);

            if (user == null) return View();

            var identity = await userManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie
            );

            identity.AddClaim(new Claim("IsAdministrator",
                user.IsAdministrator.ToString()));

            identity.AddClaim(new Claim("Email", user.Email));

            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignIn(identity);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login");
        }
    }
}