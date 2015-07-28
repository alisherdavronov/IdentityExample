using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentityExample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using IdentityExample.Helpers;

namespace IdentityExample.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginForm model)
        {
            if (ModelState.IsValid)
            {
                var claims = new Claim[0];

                if (model.Email == "admin@gmail.com" && model.Password == "1")
                {
                    claims = new[]
                    {
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Role, Roles.User),
                        new Claim(ClaimTypes.Role, Roles.Admin),
                    };
                }

                if (model.Email == "user@gmail.com" && model.Password == "1")
                {
                    claims = new[]
                    {
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Role, Roles.User),
                    };
                }

                if (claims.Length > 0)
                {
                    var identity = new ClaimsIdentity(claims, 
                        DefaultAuthenticationTypes.ApplicationCookie, 
                        ClaimTypes.Email, ClaimTypes.Role);

                    var options = new AuthenticationProperties {IsPersistent = false};
                    
                    Request.GetOwinContext().Authentication.SignIn(options, identity);
                }

                ModelState.AddModelError("", "Incorrect email and/or password");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            var auth = Request.GetOwinContext().Authentication;
            auth.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}