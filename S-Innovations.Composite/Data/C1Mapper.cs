
using SInnovations.Composite.Data.Mapping;
using SInnovations.Composite.Data.Mapping.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SInnovations.Composite.Data
{

    public static class C1Mapper
    {       

        private static Lazy<IMappingCache> _mappingCache = new Lazy<IMappingCache>(() => new MappingCache());


        public static IMappingCache Cache
        {
            get
            {
                return _mappingCache.Value;
            }
        }
    }


    public interface IMyPICO
    {
        string Name { get; set; }
    }
    public class MyPICO
    {
        string Name { get; set; }
    }

    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Add(T item);
        void Remove(T item);
        bool Update(T item);
    }

    public class Mapping<C1DATA, DTO>
    {
        private static MappingCache _cache = new MappingCache();

        private class MappingCache
        {
            private ConcurrentDictionary<Type, ConcurrentDictionary<Type, Mapping<C1DATA, DTO>>> _cache;
            public MappingCache()
            {
                _cache = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, Mapping<C1DATA, DTO>>>();
            }
        }


    }

}