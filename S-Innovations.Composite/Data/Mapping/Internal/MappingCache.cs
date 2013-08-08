using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace SInnovations.Composite.Data.Mapping.Internal
{
    public class MappingCache : IMappingCache
    {
        private readonly ConcurrentDictionary<TypePair, LambdaExpression> _expressionCache;

        public MappingCache()
        {
            _expressionCache = new ConcurrentDictionary<TypePair, LambdaExpression>();
        }

        public Expression<Func<TSource, TDestination>> GetOrCreateMapExpression<TSource, TDestination>()
        {
            return (Expression<Func<TSource, TDestination>>)
                _expressionCache.GetOrAdd(new TypePair(typeof(TSource), typeof(TDestination)), tp =>
                {
                    return CreateMapExpression(tp.SourceType, tp.DestinationType);
                });
        }

        private LambdaExpression CreateMapExpression(Type source, Type destination)
        {
            ParameterExpression instanceParameter = Expression.Parameter(source);
            var instance2Parameter = Expression.New(destination);
            LabelTarget returnTarget = Expression.Label(destination);

            var sourceMembers = source.GetProperties().Where(p => p.GetMethod.IsPublic);
            var destMembers = destination.GetProperties().Where(p => p.SetMethod.IsPublic);
            var matchingMembers = sourceMembers.Select(s =>
                new
                {
                    Source = s,
                    Dest = destMembers.FirstOrDefault(d =>
                        d.Name.Equals(s.Name) && d.PropertyType == s.PropertyType)
                }).Where(map => map.Dest != null).ToArray();


            return Expression.Lambda( Expression.MemberInit(Expression.New(destination),
                matchingMembers.Select(p =>
                    Expression.Bind(p.Dest, Expression.Property(instanceParameter, p.Source)))),
                    instanceParameter);

            //var block = Expression.Block(Expression.Block(
            //    matchingMembers.Select(p =>
            //        Expression.Assign(
            //            Expression.Property(instance2Parameter, p.Dest),
            //            Expression.Property(instanceParameter, p.Source)))),
            //             Expression.Label(returnTarget, instance2Parameter));


            //return Expression.Lambda(block, instanceParameter);
        }
    }
}
