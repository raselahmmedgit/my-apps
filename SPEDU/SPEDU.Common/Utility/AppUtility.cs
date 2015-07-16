using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SPEDU.Common.Utility
{
    public static class AppUtility
    {

        public static string GetPhysicalFilePath(string virtualDirPath, string fileName)
        {
            string physicalPath = HttpContext.Current.Server.MapPath(virtualDirPath);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
            if (!String.IsNullOrEmpty(fileName))
            {
                physicalPath = Path.Combine(physicalPath, fileName);
            }
            return physicalPath;
        }

        public static string CreateDirectory(string directoryName)
        {
            try
            {
                directoryName = HttpContext.Current.Server.MapPath(directoryName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                return directoryName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string DeleteDirectory(string directoryName)
        {
            try
            {
                directoryName = HttpContext.Current.Server.MapPath(directoryName);
                if (Directory.Exists(directoryName))
                {
                    Directory.Delete(directoryName);
                }
                return directoryName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GenerateRandomString(int length)
        {
            string[] characters = new string[82] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "<", ">", ",", ".", "{", "}" };

            Random random = new Random();
            string randomStr = "";
            for (int i = 0; i < length; i++)
            {
                randomStr += characters[Math.Abs(random.Next(-81, 81))];
            }

            return randomStr;
        }

        public static string GetDisplayId(string key)
        {
            string displayId = key + DateTime.Now.ToString("yyMMddhhmmss");
            return displayId;
        }
    }
}
