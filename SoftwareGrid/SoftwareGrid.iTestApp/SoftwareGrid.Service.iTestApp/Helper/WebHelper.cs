using System.Diagnostics;
using System.Web;
using SoftwareGrid.Model.iTestApp.SecurityManagement;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public sealed partial class WebHelper
    {
       
        public static class CurrentSession
        {
            public static string Id
            {
                get
                {
                    if (HttpContext.Current != null && HttpContext.Current.Session != null)
                    {
                        return HttpContext.Current.Session.SessionID;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
          
            public static int Timeout
            {
                get
                {
                    if (HttpContext.Current != null && HttpContext.Current.Session != null)
                    {
                        return HttpContext.Current.Session.Timeout;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
            }

            [DebuggerStepThrough()]
            public static void Clear()
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Clear();
                }
            }
           
            [DebuggerStepThrough()]
            public static void Restart()
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Abandon();
                }
            }
           
            [DebuggerStepThrough()]
            public static object Get(string key)
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    return HttpContext.Current.Session[BuildFullKey(key)];
                }
                else
                {
                    return null;
                }
            }
           
            [DebuggerStepThrough()]
            public static void Set(string key,
            object value)
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (value == null)
                    {
                        HttpContext.Current.Session.Remove(BuildFullKey(key));
                    }
                    else
                    {
                        HttpContext.Current.Session[BuildFullKey(key)] = value;
                    }
                }
            }
           
            [DebuggerStepThrough()]
            public static void Remove(string key)
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Remove(BuildFullKey(key));
                }
            }
           
            [DebuggerStepThrough()]
            public static bool Contains(string key)
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[BuildFullKey(key)] == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

          
            [DebuggerStepThrough()]
            private static string BuildFullKey(string localKey)
            {
                const string SESSION_KEY = "Web.UI.";

                if (localKey.IndexOf(SESSION_KEY) > -1)
                {
                    return localKey;
                }
                else
                {
                    return SESSION_KEY + localKey;
                }
            }

           
            [DebuggerStepThrough()]
            public static string GetString(string key)
            {
                string fullKey = BuildFullKey(key);
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[fullKey] != null)
                {
                    return HttpContext.Current.Session[fullKey].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            [DebuggerStepThrough()]
            public static string GetNullString(string key)
            {
                string fullKey = BuildFullKey(key);
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[fullKey] != null)
                {
                    return HttpContext.Current.Session[fullKey].ToString();
                }
                else
                {
                    return null;
                }
            }

            public static class Content
            {
                public static object Data
                {
                    get
                    {
                        return (object)Get("Data");
                    }
                    set
                    {
                        Set("Data", value);
                    }
                }

                public static User LoggedInUser
                {
                    get
                    {
                        return (User)Get("LoggedInUser");
                    }
                    set
                    {
                        Set("LoggedInUser", value);
                    }
                }

               
            }
        }
    }
}
