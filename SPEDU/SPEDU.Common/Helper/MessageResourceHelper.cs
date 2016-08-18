using System;
using System.Resources;
using System.Threading;

namespace SPEDU.Common.Helper
{
    public static class MessageResourceHelper
    {
        public static string Save { get { return GetValue("Save"); } }
        public static string Update { get { return GetValue("Update"); } }
        public static string Delete { get { return GetValue("Delete"); } }
        public static string Add { get { return GetValue("Add"); } }
        public static string Edit { get { return GetValue("Edit"); } }
        public static string Remove { get { return GetValue("Remove"); } }
        public static string UnhandelledError { get { return GetValue("UnhandelledError"); } }
        public static string UnAuthenticated { get { return GetValue("UnAuthenticated"); } }
        public static string NullError { get { return GetValue("NullError"); } }
        public static string NullReferenceExceptionError { get { return GetValue("NullReferenceExceptionError"); } }

        #region Get Resource Value

        private static ResourceManager _appResourceManager = null;
        private static ResourceManager AppResourceManager
        {
            get { return _appResourceManager ?? (_appResourceManager = new ResourceManager(typeof(MessageResource))); }
        }
        public static string GetValue(string keyName)
        {
            string content = AppResourceManager.GetString(keyName, Thread.CurrentThread.CurrentCulture);
            if (String.IsNullOrEmpty(content))
            {
                content = AppResourceManager.GetString(keyName, new System.Globalization.CultureInfo("en"));
            }
            return content;
        }
        public static string GetValue(string keyName, string languageCode)
        {
            if (String.IsNullOrEmpty(languageCode))
            {
                return GetValue(keyName);
            }
            else
            {
                string content = AppResourceManager.GetString(keyName, new System.Globalization.CultureInfo(languageCode));
                if (String.IsNullOrEmpty(content))
                {
                    content = AppResourceManager.GetString(keyName, new System.Globalization.CultureInfo("en"));
                }
                return content;
            }
        }

        #endregion

    }
}
