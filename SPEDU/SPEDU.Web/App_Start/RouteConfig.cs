using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SPEDU.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "HomeRoute",
             url: "Home",
             defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
               name: "LoginRoute",
               url: "Login",
               defaults: new { controller = "Home", action = "Login" }
           );

            routes.MapRoute(
              name: "RegisterRoute",
              url: "Register",
              defaults: new { controller = "Home", action = "Register" }
          );

            routes.MapRoute(
              name: "LogoutRoute",
              url: "Logout",
              defaults: new { controller = "Home", action = "Logout" }
          );

            routes.MapRoute(
              name: "UnauthorizedRoute",
              url: "Unauthorized",
              defaults: new { controller = "Home", action = "Unauthorized" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
