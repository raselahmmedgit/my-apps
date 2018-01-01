using System;
using System.Configuration;
using System.Web;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public static class MiscUtility
    {
        public static DateTime GetLocalDateTimeFromUtc(DateTime utcDateTime)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["LocalTimeZone"];
                if (cookie == null)
                {
                    return utcDateTime;
                }
                string zone = cookie.Value;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(zone);
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, cstZone);
                return cstTime;
            }
            catch (Exception exception)
            {
                return utcDateTime;
            }
        }

        public static string GetLocalTimeZone()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["LocalTimeZone"];
                return cookie == null ? string.Empty : cookie.Value;
            }
            catch (Exception exception)
            {
                return string.Empty;
            }
        }

        public static string GetTimeZoneOffset()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["TimeZoneOffset"];
                return cookie == null ? "0" : cookie.Value;
            }
            catch (Exception exception)
            {
                return "0";
            }
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
