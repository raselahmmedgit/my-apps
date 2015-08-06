using System;
using System.Linq;
using System.Web.Mvc;

namespace SPEDU.Common.Helper
{
    public static class ExceptionHelper
    {
        public static string Manage(Exception ex, bool log = false)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = ResourceHelper.GetValue("DbExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.UpdateException)
                //    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.EntityException)
                //    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
                else if (inner is NullReferenceException)
                    message = ResourceHelper.GetValue("NullReferenceExceptionError") + inner.Message;
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = ResourceHelper.GetValue("ApplicationExceptionError") + inner.Message;
                else
                    message = inner.Message;

            }

            if (log)
            {
                LoggerHelper.ErrorLog(ex);
            }

            return message;
        }

        public static string ExceptionMessageFormat(Exception ex, bool log = false)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = ResourceHelper.GetValue("DbExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.UpdateException)
                //    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.EntityException)
                //    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
                else if (inner is NullReferenceException)
                    message = ResourceHelper.GetValue("NullReferenceExceptionError") + inner.Message;
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = ResourceHelper.GetValue("ApplicationExceptionError") + inner.Message;
                else
                    message = inner.Message;

            }

            if (log)
            {
                LoggerHelper.ErrorLog(ex);
            }

            return message;
        }

        public static string ExceptionMessageForNullObject()
        {
            string message = ResourceHelper.GetValue("NullError");

            return message;
        }

        public static string ExceptionErrorMessageFormat(Exception ex)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = ResourceHelper.GetValue("DbExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.UpdateException)
                //    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                //else if (inner is System.Data.Entity.Core.EntityException)
                //    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
                else if (inner is NullReferenceException)
                    message = ResourceHelper.GetValue("NullReferenceExceptionError") + inner.Message;
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = ResourceHelper.GetValue("ApplicationExceptionError") + inner.Message;
                else
                    message = inner.Message;

            }

            return message;
        }

        public static string ExceptionErrorMessageForNullObject()
        {
            string message = ResourceHelper.GetValue("NullError");

            return message;
        }

        public static string ExceptionErrorMessageForCommon()
        {
            string message = ResourceHelper.GetValue("CommonError");

            return message;
        }

        public static string ModelStateErrorFormat(System.Web.Mvc.ModelStateDictionary modelStateDictionary)
        {
            string message = @"<div class='mess'>";

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    foreach (var modelError in modelStateValues.Errors)
                    {
                        message += "<p>";
                        message += modelError.ErrorMessage;
                        message += "</p>";
                    }
                }
            }

            message += "</div>";

            return message;
        }

    }
}
