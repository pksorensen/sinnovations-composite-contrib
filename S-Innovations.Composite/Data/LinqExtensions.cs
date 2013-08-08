using SInnovations.Composite.Data.Mapping;
using SInnovations.Composite.Data.Mapping.Internal;
using System.Linq;

namespace SInnovations.Composite.Data
{
    public static class LinqExtensions
    {
        public static IMapExpression Map<TSource>(this IQueryable<TSource> source)
        {
            return source.Map(C1Mapper.Cache);
        }
        public static IMapExpression Map<TSource>(
            this IQueryable<TSource> source, IMappingCache _cache)
        {
            return new MapExpression<TSource>(source, _cache);
        }


        public static IQueryable<TResult> MapTo<TSource, TResult>(this IQueryable<TSource> source)
        {
            return source.Map<TSource>().To<TResult>();
        }

       
    }
}
