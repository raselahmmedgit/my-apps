using System;
using System.Reflection;
using System.Linq.Expressions;

namespace SPEDU.Common.Helper
{
    public static class ObjectHelper
    {
        public static string GetName<T>(Expression<Func<T>> memberExpression)
        {
            var expression = memberExpression.Body as MemberExpression;
            if (expression != null)
            {
                return expression.Member.Name;
            }
            else
            {
                return expression.ToString();
            }
        }

        public static void CopyPropertiesValueFromBaseType<TEntity>(TEntity baseSource, TEntity destinationChild)
        {
            Type type = typeof(TEntity);

            PropertyInfo[] myObjectFields = type.GetProperties(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fi in myObjectFields)
            {
                fi.SetValue(destinationChild, fi.GetValue(baseSource, null), null);
            }
        }
    }
}
