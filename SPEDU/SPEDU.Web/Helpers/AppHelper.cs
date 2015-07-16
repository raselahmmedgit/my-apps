using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SPEDU.Web.Helpers
{
    public static class AppHelper
    {
        public static List<SelectListItem> GetAreaList()
        {
            var areaList = new List<SelectListItem>();
            Assembly assembly = Assembly.Load("SPEDU.Web");

            IEnumerable<String> areas = assembly.GetTypes()
                                                .Select(t => t.Namespace)
                                                .Where(t => t != null && t.Contains("Areas") && t.Contains("Controllers"))
                                                .Distinct();
            foreach (String area in areas)
            {
                SelectListItem currentItem = new SelectListItem();
                currentItem.Text = area.Substring(area.IndexOf("Areas."), area.IndexOf(".Controllers"));
                currentItem.Value = currentItem.Text;
                areaList.Add(currentItem);
            }
            areaList.Add(new SelectListItem { Text = "External", Value = "external" });
            areaList = areaList.OrderBy(item => item.Text).ToList();
            return areaList;
        }

        public static List<SelectListItem> GetControllerList(string areaName)
        {
            var controllerList = new List<SelectListItem>();
            if (!String.IsNullOrEmpty(areaName))
            {
                Assembly assembly = Assembly.Load("SPEDU.Web");
                IEnumerable<Type> controllerTypes = null;
                if (!String.IsNullOrEmpty(areaName) && areaName != "external")
                {
                    controllerTypes = from t in assembly.GetExportedTypes()
                                      where typeof(IController).IsAssignableFrom(t) && t.Namespace.Contains(areaName)
                                      orderby t.Name ascending
                                      select t;
                }
                else
                {
                    controllerTypes = from t in assembly.GetExportedTypes()
                                      where typeof(IController).IsAssignableFrom(t) && t.Namespace.Contains("SPEDU.Web.Controllers")
                                      orderby t.Name ascending
                                      select t;
                }

                foreach (var controllerType in controllerTypes)
                {
                    SelectListItem currentItem = new SelectListItem();
                    currentItem.Text = controllerType.Name.Replace("Controller", string.Empty);
                    currentItem.Value = currentItem.Text;
                    controllerList.Add(currentItem);
                }
            }

            return controllerList;
        }

        public static List<SelectListItem> GetActionList(string controllerName)
        {
            var actionList = new List<SelectListItem>();
            if (!String.IsNullOrEmpty(controllerName))
            {
                Assembly assembly = Assembly.Load("SPEDU.Web");

                IEnumerable<Type> controllerTypes = from t in assembly.GetExportedTypes()
                                                    where typeof(IController).IsAssignableFrom(t) && t.Name.Contains(controllerName + "Controller")
                                                    select t;
                foreach (var controllerType in controllerTypes)
                {
                    List<string> actionNames = new List<string>();
                    MethodInfo[] mi = controllerType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    foreach (MethodInfo m in mi)
                    {
                        if (m.IsPublic)
                        {
                            if (typeof(ActionResult).IsAssignableFrom(m.ReturnParameter.ParameterType))
                            {
                                bool isNotExist = true;
                                foreach (SelectListItem item in actionList)
                                {
                                    if (item.Text == m.Name)
                                    {
                                        isNotExist = false;
                                        break;
                                    }
                                }
                                if ((isNotExist && !m.Name.Contains("Delete") && !m.Name.Contains("Ajax")))
                                {
                                    SelectListItem currentItem = new SelectListItem();
                                    currentItem.Text = m.Name;
                                    currentItem.Value = m.GetActionName();
                                    actionList.Add(currentItem);
                                }
                            }
                        }
                    }
                }
            }
            actionList = actionList.OrderBy(item => item.Text).ToList();
            return actionList;
        }

        public static string GetActionName(this MethodInfo m)
        {
            bool hasAttribute = false;
            try
            {
                hasAttribute =
               m.GetCustomAttributes(typeof(ActionNameAttribute), false).Any();

            }
            catch (Exception)
            {
                return m.Name;

            }
            if (hasAttribute)
            {
                ActionNameAttribute attribute = (ActionNameAttribute)m.GetCustomAttributes(typeof(ActionNameAttribute), false).First();
                return attribute.Name;
            }
            else
            {
                return m.Name;
            }

        }
    }
}