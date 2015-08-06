using System;
using System.Resources;
using System.Threading;

namespace SPEDU.Common.Helper
{
    public static class ResourceHelper
    {
        public static string Save = GetValue("Save");
        public static string Update = GetValue("Update");
        public static string Delete = GetValue("Delete");
        public static string Add = GetValue("Add");
        public static string Edit = GetValue("Edit");
        public static string Remove = GetValue("Remove");

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
