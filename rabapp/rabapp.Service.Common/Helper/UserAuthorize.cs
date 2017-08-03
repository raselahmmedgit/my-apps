using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Service.Common.Helper
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    UserViewModel loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;

                    if (loggedInUser == null)
                    {
                        var userName = GetClaimInformation("identity/claims/emailaddress");
                        if (!string.IsNullOrEmpty(userName))
                        {
                            var userRepository = DependencyResolver.Current.GetService(typeof(IUserRepository)) as IUserRepository;
                            if (userRepository != null) loggedInUser = userRepository.GetUserByUserName(userName);

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
