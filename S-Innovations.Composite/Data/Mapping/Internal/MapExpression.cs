using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SInnovations.Composite.Data.Mapping.Internal
{
    public class MapExpression<TSource> : IMapExpression
    {
        private readonly IQueryable<TSource> _source;
        private readonly IMappingCache _cache;

        public MapExpression(IQueryable<TSource> source, IMappingCache cache)
        {
            _source = source;
            _cache = cache;

        }

        public IQueryable<TResult> To<TResult>()
        {
            Expression<Func<TSource, TResult>> expr = _cache.GetOrCreateMapExpression<TSource, TResult>();
            return _source.Select(expr);
        }
    }
}
