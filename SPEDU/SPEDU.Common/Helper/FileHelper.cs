using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SPEDU.Common.Helper
{
    public static class FileHelper
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
    }
}
