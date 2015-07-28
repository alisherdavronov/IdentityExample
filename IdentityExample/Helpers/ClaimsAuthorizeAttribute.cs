using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityExample.Helpers
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimType;
        private readonly string _claimValue;
        
        public ClaimsAuthorizeAttribute(string type, string value)
        {
            _claimType = type;
            _claimValue = value;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user == null)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else if (user.HasClaim(_claimType, _claimValue))
            {
                base.OnAuthorization(filterContext);
            }
            else if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int) System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}