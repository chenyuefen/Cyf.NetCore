using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Collections.Concurrent;

namespace Helpers
{
    public class AutoMapperHelper
    {
    }

    public static class AutoMapperHelperExt
    {
        private static ConcurrentDictionary<Type, IMapper> mapperCache = new ConcurrentDictionary<Type, IMapper>();
        public static T CloneByMapper<T>(this T obj)
        {
            var mapper = mapperCache.GetOrAdd(typeof(T), x => new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap(x, x);
               }).CreateMapper());
            return mapper.Map<T>(obj);
        }
    }
}
