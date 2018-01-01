using System.Web;
using SoftwareGrid.Model.Utility;

namespace SoftwareGrid.Service.Base
{
    public class CurrentSession
    {
        public static AppSession GetCurrentSession()
        {
            AppSession vmSession;

            if (HttpContext.Current.Session["Session"] != null)
            {
                vmSession = HttpContext.Current.Session["Session"] as AppSession;
            }
            else
            {
                vmSession = null;
                vmSession = HttpContext.Current.Session["Session"] as AppSession;

            }
            return vmSession;
        }
    }
}
