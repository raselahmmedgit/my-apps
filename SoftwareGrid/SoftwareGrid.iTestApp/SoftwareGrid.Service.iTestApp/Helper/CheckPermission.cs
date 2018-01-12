using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public class CheckPermission : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            User loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;
            if (loggedInUser != null && loggedInUser.UserType == (int)AppUsers.User)
            {
                filterContext.Result = new RedirectResult("/Unauthorized");
            }
            base.OnAuthorization(filterContext);
           
        }
    }
}
