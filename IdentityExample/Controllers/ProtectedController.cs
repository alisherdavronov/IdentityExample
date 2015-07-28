using System.Security.Claims;
using System.Web.Mvc;
using IdentityExample.Helpers;

namespace IdentityExample.Controllers
{
    public class ProtectedController : Controller
    {
        //[Authorize(Roles = Roles.Admin)]
        [ClaimsAuthorize(ClaimTypes.Role, Roles.Admin)]
        public ActionResult ForAdmin()
        {
            return View();
        }

        //[Authorize(Roles = Roles.User)]
        [ClaimsAuthorize(ClaimTypes.Role, Roles.User)]
        public ActionResult ForUser()
        {
            return View();
        }

    }
}