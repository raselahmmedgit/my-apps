using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SPEDU.Domain.BaseModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            DeletedDate = DateTime.Now;
            IsDelete = false;

            var httpContext = HttpContext.Current;
            var httpContextBase = new HttpContextWrapper(httpContext);
            string areaName = httpContextBase.Request.RequestContext.RouteData.DataTokens.ContainsKey("area") ? httpContextBase.Request.RequestContext.RouteData.DataTokens["area"].ToString() : "";
            string controllerName = httpContextBase.Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = httpContextBase.Request.RequestContext.RouteData.Values["action"].ToString();

            this.AreaName = areaName;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }

        //public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool IsDelete { get; set; }

        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public virtual string ActionLink { get; set; }

        public virtual bool HasCreate { get; set; }

        public virtual bool HasUpdate { get; set; }

        public virtual bool HasDelete { get; set; }

        #endregion
    }
}
