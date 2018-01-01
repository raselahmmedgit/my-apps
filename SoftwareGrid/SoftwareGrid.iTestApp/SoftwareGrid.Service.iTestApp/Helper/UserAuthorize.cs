using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Service.iTestApp.UserManagement;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    User loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;

                    if (loggedInUser == null)
                    {
                        var userName = GetClaimInformation("identity/claims/emailaddress");
                        if (!string.IsNullOrEmpty(userName))
                        {
                            var userService = DependencyResolver.Current.GetService(typeof(IUserService)) as IUserService;
                            loggedInUser = userService.GetUser(userName);

                            if (loggedInUser == null)
                            {
                                filterContext.Result = new RedirectResult("/LogOff");
                            }
                        }
                        WebHelper.CurrentSession.Content.LoggedInUser = loggedInUser;
                    }
                    if (loggedInUser != null)
                    {
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Login");
                    }
                }
                base.OnAuthorization(filterContext);
            }
        }

        private string GetClaimInformation(string claimType)
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type.Contains(claimType));
            return claim == null ? null : claim.Value;
        }
    }
}
