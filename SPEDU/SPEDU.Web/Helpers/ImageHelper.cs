using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SPEDU.Web.Helpers
{
    public static class ImageHelper
    {
        public static string GetMIMEType(string documentName)
        {
            return MimeMapping.GetMimeMapping(documentName);
        }

        public static void WriteFile(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }
    }
}