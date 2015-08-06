using System;
using System.Web;

namespace SPEDU.Common.Helper
{
    public class LoggerHelper
    {
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ErrorLog()
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            ErrorLog(ex);
        }

        public static void ErrorLog(Exception ex)
        {
            _logger.Error("Error Log: ", ex);
        }

        public static void InfoLog(Exception ex)
        {
            _logger.Info("Info Log: ", ex);
        }

        public static void WarnLog(Exception ex)
        {
            _logger.Warn("Info Log: ", ex);
        }
    }
}
