using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SInnovations.Composite.Data.Mapping
{
    public interface IMappingCache
    {

        Expression<Func<TSource, TResult>> GetOrCreateMapExpression<TSource, TResult>();
    }
}
