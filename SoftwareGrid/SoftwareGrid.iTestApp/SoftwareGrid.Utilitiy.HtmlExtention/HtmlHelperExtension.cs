using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftwareGrid.Utilitiy.HtmlExtention
{
    public static class HtmlHelperExtension
    {
        #region Page Title

        public static IHtmlString RenderPageTitle(this HtmlHelper htmlHelper)
        {
            //string pageTitle = SiteConfigurationReader.GetAppSettingsString("PageTitle") + " | ";

            return MvcHtmlString.Create("");
        }


        #endregion Logo

        #region Logo

        public static IHtmlString RenderInternalLogo(this HtmlHelper htmlHelper)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string indexUrl = String.Empty;
            string logoPath = String.Empty;
            string logoAlt = String.Empty;

            //if (CurrentDomain.CurrentApplicationDomain == ApplicationDomain.RBPlatform)
            //{
            //    logoPath += "/assets/frontend/logo.png";
            //}
            //else
            //{
            //    logoPath += "/assets/recruiting-deal/images/logo-md.png";
            //}

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                indexUrl += "/Home/Index";
            }
            else
            {
                indexUrl += "/home";
            }

            logoAlt += "logo";

            stringBuilder.Append(@"<div class='page-logo'>");
            stringBuilder.Append(@"<a href='" + indexUrl + "'>");
            stringBuilder.Append(@"<img class='logo-default' src='" + logoPath + "' alt='" + logoAlt + "' />");
            stringBuilder.Append(@"</a>");
            //stringBuilder.Append(@"<div class='menu-toggler sidebar-toggler'>");
            //stringBuilder.Append(@"</div>");
            stringBuilder.Append(@"</div>");

            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        public static IHtmlString RenderLoginLogo(this HtmlHelper htmlHelper)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string indexUrl = String.Empty;
            string logoPath = String.Empty;
            string logoAlt = String.Empty;

            //if (CurrentDomain.CurrentApplicationDomain == ApplicationDomain.RBPlatform)
            //{
            //    logoPath += "/assets/frontend/logo.png";
            //}
            //else
            //{
            //    logoPath += "/assets/recruiting-deal/images/logo-md.png";
            //}

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                indexUrl += "/Home/Index";
            }
            else
            {
                indexUrl += "/home";
            }

            logoAlt += "logo";

            stringBuilder.Append(@"<div class='logo'>");
            stringBuilder.Append(@"<a href='" + indexUrl + "'>");
            stringBuilder.Append(@"<img  src='" + logoPath + "' alt='" + logoAlt + "' />");
            stringBuilder.Append(@"</a>");
            stringBuilder.Append(@"</div>");

            return MvcHtmlString.Create(stringBuilder.ToString());
        }


        #endregion Logo

        #region Dashboard Breadcrumb

        public static IHtmlString RenderDashboardBreadcrumb(this HtmlHelper htmlHelper)
        {
            StringBuilder stringBuilder = new StringBuilder();

            object objCurrentControllerName = string.Empty;
            HttpContext.Current.Request.RequestContext.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
            object objCurrentActionName = string.Empty;
            HttpContext.Current.Request.RequestContext.RouteData.Values.TryGetValue("action", out objCurrentActionName);
            string currentAreaName = string.Empty;
            if (HttpContext.Current.Request.RequestContext.RouteData.DataTokens.ContainsKey("area"))
            {
                currentAreaName = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString();
            }
            string currentActionName = objCurrentActionName.ToString();
            string currentControllerName = objCurrentControllerName.ToString();

            string homeUrl = "/Home/Dashboard";
            //string controllerUrl = GetBreadcrumbUrl(currentAreaName, currentControllerName, currentActionName);
            string controllerUrl = "#";
            string actionUrl = GetBreadcrumbUrl(currentAreaName, currentControllerName, currentActionName);

            string homeUrlText = "Home";
            string controllerUrlText = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(currentControllerName.ToLower()); ;
            string actionUrlText = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(currentActionName.ToLower());

            if (homeUrlText == controllerUrl)
            {
                stringBuilder.Append(@"<div class='page-bar'>");
                stringBuilder.Append(@"<ul class='page-breadcrumb'>");
                stringBuilder.Append(@"<li><i class='fa fa-home'></i>");
                stringBuilder.Append(homeUrlText);
                stringBuilder.Append(@"</li>");
                stringBuilder.Append(@"</ul>");
                stringBuilder.Append(@"</div>");
            }
            else
            {
                stringBuilder.Append(@"<div class='page-bar'>");
                stringBuilder.Append(@"<ul class='page-breadcrumb'>");

                stringBuilder.Append(@"<li><i class='fa fa-home'></i>");
                stringBuilder.Append(@"<a href='" + homeUrl + "'>");
                stringBuilder.Append(homeUrlText);
                stringBuilder.Append(@"</a>");
                stringBuilder.Append(@"<i class='fa fa-angle-right'></i>");
                stringBuilder.Append(@"</li><li>");
                stringBuilder.Append(@"<a href='" + controllerUrl + "'>");
                stringBuilder.Append(controllerUrlText);
                stringBuilder.Append(@"</a>");
                //stringBuilder.Append(@"<i class='fa fa-angle-right'></i>");
                //stringBuilder.Append(@"</li><li>");
                //stringBuilder.Append(actionUrlText);
                stringBuilder.Append(@"</li>");
                stringBuilder.Append(@"</ul>");
                stringBuilder.Append(@"</div>");
            }

            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        private static string GetBreadcrumbUrl(string areaName, string controllerName, string actionName)
        {
            return (!string.IsNullOrEmpty(areaName) ?
                "/" + areaName : string.Empty) + (!string.IsNullOrEmpty(controllerName) ?
                    "/" + controllerName : string.Empty) + (!string.IsNullOrEmpty(actionName) ?
                        "/" + actionName : string.Empty);
        }

        #endregion Dashboard Breadcrumb

        #region Dashboard Top

        public static IHtmlString RenderTopSearchDropdown(this HtmlHelper htmlHelper)
        {
            string strContent = String.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            string applicantCount = "1245";
            string employeeCount = "9999";
            string contactCount = "4454";

            stringBuilder.Append(@"<div class='page-actions'>");
            stringBuilder.Append(@"<div class='btn-group'>");
            stringBuilder.Append(@"<button type='button' class='btn dropdown-toggle' data-toggle='dropdown'>");
            stringBuilder.Append(@"<i class='fa fa-search'></i>&nbsp;<span class='hidden-sm hidden-xs'>&nbsp;</span>&nbsp;<i class='fa fa-angle-down'></i>");
            stringBuilder.Append(@"</button>");
            stringBuilder.Append(@"<ul class='dropdown-menu' role='menu'>");
            stringBuilder.Append(@"<li>");
            stringBuilder.Append(@"<a href='javascript:;'>");
            stringBuilder.Append(@"<i class='icon-user'></i> Applicant <span class='badge badge-success'>" + applicantCount + "</span>");
            stringBuilder.Append(@"</a>");
            stringBuilder.Append(@"</li>");
            stringBuilder.Append(@"<li>");
            stringBuilder.Append(@"<a href='javascript:;'>");
            stringBuilder.Append(@"<i class='icon-user'></i> Employee <span class='badge badge-success'>" + employeeCount + "</span>");
            stringBuilder.Append(@"</a>");
            stringBuilder.Append(@"</li>");
            stringBuilder.Append(@"<li>");
            stringBuilder.Append(@"<a href='javascript:;'>");
            stringBuilder.Append(@"<i class='icon-user'></i> Contact <span class='badge badge-success'>" + contactCount + "</span>");
            stringBuilder.Append(@"</a>");
            stringBuilder.Append(@"</li>");
            stringBuilder.Append(@"</ul>");
            stringBuilder.Append(@"</div>");
            stringBuilder.Append(@"</div>");

            return MvcHtmlString.Create(strContent);
        }

        #endregion Dashboard Top

       
    }
}
