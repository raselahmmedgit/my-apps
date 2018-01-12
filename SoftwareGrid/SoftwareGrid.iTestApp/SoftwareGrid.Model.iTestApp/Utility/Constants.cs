using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGrid.Model.iTestApp.Utility
{
    public static class Constants
    {
        public static class CookieKeys
        {
            public const string Culture = "Culture";
            public const string Currency = "Currency";
            public const string Marketplace = "Marketplace";
            public const string UserAuthentication = "UserSession";
        }
        public static class CommonConstants
        {
            public const string RememberMeCookieName = ".RBCOOKIE";
            public const string DefaultControllerName = "Home";
            public const string DefaultActionName = "Index";
            public const int DefaultPageSize = 20;
        }

        public static class ValidFileExtension
        {
            public static string[] fileExtension = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".txt", ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".csv", ".wmv", ".ppt", ".pptx", ".rar", ".zip" };
        }

        public static class ImagePath
        {
            public const string ImageFolderPath = "~/Download/Image/";
            public const string TemporaryImageFolderPath = "~/Download/Temporary/";
            public const string TestIconFolderPath = "~/Download/TestIcon/";
            public const string QuestionImageFolderPath = "~/Download/QuestionImage/";
        }

    }
}
