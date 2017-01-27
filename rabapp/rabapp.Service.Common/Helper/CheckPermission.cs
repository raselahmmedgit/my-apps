using rabapp.Model.Common;
using rabapp.Model.Quiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace rabapp.Service.Common.Helper
{
    public class CheckPermission : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UserViewModel loggedInUser = WebHelper.CurrentSession.Content.LoggedInUser;
            if (loggedInUser != null)
            {
                filterContext.Result = new RedirectResult("/Unauthorized");
            }
            base.OnAuthorization(filterContext);

        }
    }
}
