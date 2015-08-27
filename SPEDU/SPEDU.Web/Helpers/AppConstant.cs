using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPEDU.Web.Helpers
{
    public static class AppConstant
    {
        static AppConstant()
        {
            var httpContext = HttpContext.Current;
            var httpContextBase = new HttpContextWrapper(httpContext);
            string areaName = httpContextBase.Request.RequestContext.RouteData.DataTokens.ContainsKey("area") ? httpContextBase.Request.RequestContext.RouteData.DataTokens["area"].ToString() : "";
            string controllerName = httpContextBase.Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = httpContextBase.Request.RequestContext.RouteData.Values["action"].ToString();
            
            AreaName = areaName;
            ControllerName = controllerName;
            ActionName = actionName;
            
        }

        public static Boolean IsAdd()
        {
            bool isAdd;

            if (ActionName == "Add")
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }

            return isAdd;

        }

        public static Boolean IsEdit()
        {
            bool isEdit;

            if (ActionName == "Edit")
            {
                isEdit = true;
            }
            else
            {
                isEdit = false;
            }

            return isEdit;

        }

        public static String AreaName { get; set; }
        public static String ControllerName { get; set; }
        public static String ActionName { get; set; }
    }
}