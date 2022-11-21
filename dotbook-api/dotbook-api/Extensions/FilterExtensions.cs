using dotbook_api.Models.Base;
using System.Linq;

namespace dotbook_api.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, BaseQueryParams filter)
        {
            if (filter == null) return query;
            if (filter.Skip > 0)
            {
                query = query.Skip(filter.Skip);
            }
            if (filter.Take > 0)
            {
                query = query.Take(filter.Take);
            }
            return query;
        }
    }
}
