using System;
using System.Globalization;
using System.Web.Mvc;

namespace SPEDU.Web.Attribute
{
    public class CustomDateBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null)
                throw new ArgumentNullException(bindingContext.ModelName);

            var cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            //cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            try
            {
                var date = value.ConvertTo(typeof(DateTime), cultureInfo);

                return date;
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
                return null;
            }
        }
    }
}