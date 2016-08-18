using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using SPEDU.Common.Helper;
using SPEDU.Web.Helpers;
using SPEDU.Web.Attribute;
using SPEDU.Domain.Helpers;

namespace SPEDU.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(typeof(DateTime?), new CustomDateBinder());

            BootStrapper.Run();
            
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo cultureInfo = new CultureInfo("en-ZA", false)
            {
                DateTimeFormat =
                {
                    //DateSeparator = "/",
                    //ShortDatePattern = "dd/MM/yyyy",
                    //LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
                    DateSeparator = "-",
                    ShortDatePattern = "dd-MMM-yyyy",
                    LongDatePattern = "dd-MMM-yyyy hh:mm:ss tt"
                }
            };
            // NOTE: change the culture name en-ZA to whatever culture suits your needs

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // Get the exception object.
            Exception exception = Server.GetLastError();
            // Handle HTTP errors
            if (exception != null && exception.GetType() == typeof(HttpException))
            {

                ExceptionHelper.Manage(exception, true);
                
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exception.Message.Contains("NoCatch") || exception.Message.Contains("maxUrlLength"))
                { return;}

            }
            else
            {
                // Log the exception and notify system operators
                if (exception != null)
                {
                    ExceptionHelper.Manage(exception, true);
                }
                if (!HttpContext.Current.Request.Url.ToString().Contains("Error"))
                {
                    UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    Response.Redirect(urlHelper.Action("Error", "Home", new { Area = string.Empty }));
                }
            }

            // Clear the error from the server
            Server.ClearError();
        }
        protected void Application_End()
        {
            SessionHelper.CurrentSession.Clear();
        }
        
    }
}
