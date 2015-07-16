using System;
using System.Collections.Generic;
using System.Linq;

namespace SPEDU.Web.Helpers
{
    public static class LinqFilteringHelper
    {
        public static IEnumerable<T> MultipleFilter<T>(this IEnumerable<T> data,
          List<FilterDescription> filterExpressions)
        {
            if ((filterExpressions == null) || (filterExpressions.Count <= 0))
            {
                return data;
            }

            IEnumerable<T> filteredquery = from item in data select item;

            for (int i = 0; i < filterExpressions.Count; i++)
            {
                var index = i;

                Func<T, bool> expression = item =>
                {
                    var filter = filterExpressions[index];
                    var itemValue = item.GetType()
                        .GetProperty(filter.FilterField)
                        .GetValue(item, null);

                    if (itemValue == null)
                    {
                        return false;
                    }

                    var value = filter.FilterValue;
                    switch (filter.FilterOperator)
                    {
                        case "eq":
                            return itemValue.ToString() == value;
                        case "startswith":
                            return itemValue.ToString().StartsWith(value);
                        case "contains":
                            return itemValue.ToString().Contains(value);
                        case "endswith":
                            return itemValue.ToString().EndsWith(value);
                    }

                    return true;
                };

                filteredquery = filteredquery.Where(expression);
            }

            return filteredquery;
        }
    }
}