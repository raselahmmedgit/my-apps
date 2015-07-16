using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SPEDU.Domain.BaseModels
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;

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

        public Int32 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Int32 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Boolean IsDelete { get; set; }

        public Int32 DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        public Boolean IsAdd()
        {
            bool isAdd;

            if (this.ActionName == "Add")
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }

            return isAdd;

        }

        public Boolean IsEdit()
        {
            bool isEdit;

            if (this.ActionName == "Edit")
            {
                isEdit = true;
            }
            else
            {
                isEdit = false;
            }

            return isEdit;

        }

        [NotMapped]
        public String AreaName { get; set; }
        [NotMapped]
        public String ControllerName { get; set; }
        [NotMapped]
        public String ActionName { get; set; }
        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }

        #endregion

    }

    public class BaseNotMapModel
    {
        public BaseNotMapModel()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;

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
        [NotMapped]
        public Int32 CreatedBy { get; set; }
        [NotMapped]
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public Int32 UpdatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public Boolean IsDelete { get; set; }
        [NotMapped]
        public Int32 DeletedBy { get; set; }
        [NotMapped]
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        public Boolean IsAdd()
        {
            bool isAdd;

            if (this.ActionName == "Add")
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }

            return isAdd;

        }

        public Boolean IsEdit()
        {
            bool isEdit;

            if (this.ActionName == "Edit")
            {
                isEdit = true;
            }
            else
            {
                isEdit = false;
            }

            return isEdit;

        }

        [NotMapped]
        public String AreaName { get; set; }
        [NotMapped]
        public String ControllerName { get; set; }
        [NotMapped]
        public String ActionName { get; set; }
        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }

        #endregion

    }
}
