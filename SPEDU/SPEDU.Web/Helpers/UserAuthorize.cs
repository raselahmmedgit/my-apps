using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SPEDU.Web.Helpers
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            //User loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User != null)
            {
                //if (HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    User loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;
                //    if (loggedInUser == null)
                //    {
                //        IUserRepository userRepository = DependencyResolver.Current.GetService(typeof(IUserRepository)) as IUserRepository;

                //        loggedInUser = userRepository.Find(GetClaimInformation("email"));
                //        WebHelper.CurrentSession.Content.LoggedInUser = loggedInUser;
                //    }
                //    if (loggedInUser != null)
                //    {
                //        bool isDevelopmentMode = Convert.ToBoolean(ConfigurationManager.AppSettings["DevelopmentMode"]);
                //        if (!isDevelopmentMode)
                //        {
                //            var routeData = ((MvcHandler)filterContext.HttpContext.Handler).RequestContext.RouteData;
                //            object currentAreaName = string.Empty;
                //            routeData.Values.TryGetValue("area", out currentAreaName);
                //            object currentControllerName = string.Empty;
                //            routeData.Values.TryGetValue("controller", out currentControllerName);
                //            object currentActionName = string.Empty;
                //            routeData.Values.TryGetValue("action", out currentActionName);
                //            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                //            {
                //                IUserPermissionRepository userPermissionRepsitory = DependencyResolver.Current.GetService(typeof(IUserPermissionRepository)) as IUserPermissionRepository;
                //                bool hasPermission = userPermissionRepsitory.HasPermission(loggedInUser.UserID, currentAreaName.ToString(true), currentControllerName.ToString(true), currentActionName.ToString(true));
                //                if (!hasPermission)
                //                {
                //                    filterContext.Result = CreateResult(filterContext);
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        filterContext.Result = new RedirectResult("/login");
                //    }
                //}
                base.OnAuthorization(filterContext);
            }
        }

        private string GetClaimInformation(string claimType)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.Where(c => c.Type.ToLower() == claimType.ToLower()).FirstOrDefault();
            return claim == null ? null : claim.Value;
        }

        protected ActionResult CreateResult(AuthorizationContext filterContext)
        {
            var viewName = "~/Views/Shared/AccessDenied.cshtml";
            return new PartialViewResult
            {
                ViewName = viewName
            };
        }
    }
}