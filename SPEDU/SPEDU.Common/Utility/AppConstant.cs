using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEDU.Common.Utility
{
    public class AppConstant
    {
        public static class Paths
        {
            public const string TemporaryFileUploadPath = "~/UploadFile/Temporary";
            public const string DownloadFilePath = "~/Download/";
        }

       public static class RegularExpressions
       {
           public const string PhoneNumber = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
       }

       public static class ValidFileExtension
       {
           public static string[] fileExtension = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".txt", ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".csv", ".wmv", ".ppt", ".pptx", ".rar", ".zip" };
       }

       public static class ImagePath
       {
           public const string ImageFolderPath = "~/Download/Image/";
           public const string TemporaryImageFolderPath = "~/Download/Temporary/";
       }

       public static class CacheKey
       {
           public const string DefaultCacheLifeTimeInMinute = "DefaultCacheLifeTimeInMinute";
           public const string AllMenu = "AllMenu";
           public const string AllGender = "AllGender";
           public const string AllUserRole = "AllUserRole";
           public const string AllApplicationInfo = "AllApplicationInfo";
       }
    }
}
