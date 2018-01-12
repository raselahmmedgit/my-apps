using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Service.iTestApp.DI;

namespace SoftwareGrid.iTestApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbConfig.ConnectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            new ResolveDependency().Resolve();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
          
            if (exc != null && exc.GetType() == typeof(HttpException))
            {
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;
            }
            else
            {
                return;
            }
          
            Server.ClearError();
        }
    }
}
