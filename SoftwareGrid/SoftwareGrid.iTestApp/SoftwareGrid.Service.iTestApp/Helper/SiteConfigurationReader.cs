using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public static class SiteConfigurationReader
    {

        public static string GetAppSettingsString(string keyName)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(keyName);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Boolean GetAppSettingsBoolean(string keyName)
        {
            try
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings.Get(keyName));
            }
            catch
            {
                return false;
            }
        }

        public static Int32 GetAppSettingsInteger(string keyName)
        {
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings.Get(keyName));
            }
            catch
            {
                return 0;
            }
        }

    }
}
