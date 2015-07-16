using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPEDU.Web.ViewModels;

namespace SPEDU.Web.Helpers
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
                else if (inner is System.Data.Entity.Core.UpdateException)
                    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                else if (inner is System.Data.Entity.Core.EntityException)
                    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
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
                else if (inner is System.Data.Entity.Core.UpdateException)
                    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                else if (inner is System.Data.Entity.Core.EntityException)
                    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
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

        public static ErrorViewModel ExceptionErrorMessageFormat(Exception ex)
        {
            var errorViewModel = new ErrorViewModel();

            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = ResourceHelper.GetValue("DbExceptionError") + inner.Message;
                else if (inner is System.Data.Entity.Core.UpdateException)
                    message = ResourceHelper.GetValue("UpdateExceptionError") + inner.Message;
                else if (inner is System.Data.Entity.Core.EntityException)
                    message = ResourceHelper.GetValue("EntityExceptionError") + inner.Message;
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

            errorViewModel.ErrorType = MessageType.danger.ToString();
            errorViewModel.ErrorMessage = message;

            return errorViewModel;
        }

        public static ErrorViewModel ExceptionErrorMessageForNullObject()
        {
            var errorViewModel = new ErrorViewModel();

            string message = ResourceHelper.GetValue("NullError");

            errorViewModel.ErrorType = MessageType.warning.ToString();
            errorViewModel.ErrorMessage = message;

            return errorViewModel;
        }

        public static ErrorViewModel ExceptionErrorMessageForCommon()
        {
            var errorViewModel = new ErrorViewModel();

            string message = ResourceHelper.GetValue("CommonError");

            errorViewModel.ErrorType = MessageType.info.ToString();
            errorViewModel.ErrorMessage = message;

            return errorViewModel;
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