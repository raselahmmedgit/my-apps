using System;
using System.Collections.Generic;
using System.Linq;

namespace SPEDU.Web.Helpers
{
    public static class LinqSortingUtility
    {
        public static IEnumerable<T> MultipleSort<T>(this IEnumerable<T> data,
          List<SortDescription> sortExpressions)
        {
            if ((sortExpressions == null) || (sortExpressions.Count <= 0))
            {
                return data;
            }

            IEnumerable<T> query = from item in data select item;
            IOrderedEnumerable<T> orderedQuery = null;

            for (int i = 0; i < sortExpressions.Count; i++)
            {
                var index = i;
                Func<T, object> expression = item => item.GetType()
                                .GetProperty(sortExpressions[index].SortField)
                                .GetValue(item, null);

                if (sortExpressions[index].SortDir == "asc")
                {
                    orderedQuery = (index == 0)
                        ? query.OrderBy(expression)
                            : orderedQuery.ThenBy(expression);
                }
                else
                {
                    orderedQuery = (index == 0)
                        ? query.OrderByDescending(expression)
                            : orderedQuery.ThenByDescending(expression);
                }
            }

            query = orderedQuery;

            return query;
        }
    }
}