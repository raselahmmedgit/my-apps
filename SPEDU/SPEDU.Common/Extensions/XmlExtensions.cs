using System;
using System.Xml.Linq;

namespace SPEDU.Common.Extensions
{
    public static class XmlExtensions
    {
        public static string GetStringValue(this XElement element, string field)
        {
            return (element.Element(field) == null) ? null : element.Element(field).Value;
        }

        public static DateTime? GetDateTimeValue(this XElement element, string field)
        {
            if (element.Element(field) == null)
                return null;

            DateTime result = new DateTime();

            var xElement = element.Element(field);
            if (xElement != null) DateTime.TryParse(xElement.Value, out result);

            return result;
        }

        public static int? GetIntValue(this XElement element, string field)
        {
            if (element.Element(field) == null)
                return null;

            int result = 0;

            var xElement = element.Element(field);
            if (xElement != null) int.TryParse(xElement.Value, out result);

            return result;
        }

        public static byte[] GetByteArrayValue(this XElement element, string field)
        {
            return null;
        }

    }
}
