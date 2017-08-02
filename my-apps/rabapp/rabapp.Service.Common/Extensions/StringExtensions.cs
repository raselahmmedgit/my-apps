using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Service.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.Contains(value))
                        return true;
                }
            }

            return false;
        }

        public static bool ContainsAll(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (!str.Contains(value))
                        return false;
                }
            }

            return true;
        }

        public static bool IsImage(this string str)
        {
            FileInfo file = new FileInfo(str);
            string fileExtension = file.Extension.ToLower();
            if (fileExtension == ".jpeg" || fileExtension == ".gif" || fileExtension == ".png" || fileExtension == ".jpg")
            {
                return true;
            }
            return false;
        }
    }
}
