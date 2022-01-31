using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WilmerFlorez.Persistence.Extensions
{
    public static class MapperExtension
    {
        public static T MapTo<T>(this object value)
        {
            var str = JsonConvert.SerializeObject(value,
                            Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return JsonConvert.DeserializeObject<T>(
                str
            );
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
