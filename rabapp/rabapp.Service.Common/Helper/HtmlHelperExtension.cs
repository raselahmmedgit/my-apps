using rabapp.Repository.Quiz.SecurityManagement;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rabapp.Service.Common.Helper
{
    public static class HtmlHelperExtension
    {
        #region Page Title

        public static IHtmlString RenderPageTitle(this HtmlHelper htmlHelper)
        {
            string pageTitle = SiteConfigurationReader.GetAppSettingsString("PageTitle") + " | ";

            return MvcHtmlString.Create(pageTitle);
        }

        public static IHtmlString RenderMetaDescription(this HtmlHelper htmlHelper)
        {
            string pageTitle = SiteConfigurationReader.GetAppSettingsString("MetaDescription");

            return MvcHtmlString.Create(pageTitle);
        }

        public static IHtmlString RenderMetaKeywords(this HtmlHelper htmlHelper)
        {
            string pageTitle = SiteConfigurationReader.GetAppSettingsString("MetaKeywords");

            return MvcHtmlString.Create(pageTitle);
        }


        #endregion Logo

        #region Logo


        #endregion Logo

        #region Dashboard Breadcrumb

        public static IHtmlString RenderDashboardBreadcrumb(this HtmlHelper htmlHelper)
        {
            StringBuilder stringBuilder = new StringBuilder();

            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        private static string GetBreadcrumbUrl(string areaName, string controllerName, string actionName)
        {

            return (!string.IsNullOrEmpty(areaName) ? "/" + areaName : string.Empty) + (!string.IsNullOrEmpty(controllerName) ? "/" + controllerName : string.Empty) + (!string.IsNullOrEmpty(actionName) ? "/" + actionName : string.Empty);
        }

        #endregion Dashboard Breadcrumb

        #region User Login CurrentSession

        public static bool UserIdentityIsAuthenticated(this HtmlHelper htmlHelper)
        {
            var loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;
            if (loggedInUser == null)
            {
                var userName = GetClaimInformation("identity/claims/emailaddress");
                if (!string.IsNullOrEmpty(userName))
                {
                    var userRepository = DependencyResolver.Current.GetService(typeof(IUserRepository)) as IUserRepository;
                    if (userRepository != null) loggedInUser = userRepository.GetUserByUserName(userName);
                }
                WebHelper.CurrentSession.Content.LoggedInUser = loggedInUser;

                return loggedInUser != null;
            }
            return true;
        }

        private static string GetClaimInformation(string claimType)
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.Where(c => c.Type.Contains(claimType)).FirstOrDefault();
            return claim == null ? null : claim.Value;
        }

        #endregion
    }
}
