using System;
using System.Collections.Generic;
using System.Reflection;
using TM.Core.Data.Dapper.Abstractions;

namespace TM.Core.Data.Dapper.Strategy
{
    /// <summary>
    /// 随机策略
    /// </summary>
    public class RandomStrategy : IReadDbStrategy
    {
        //所有读库类型
        public static List<Type> DbTypes;

        static RandomStrategy()
        {
            LoadDbs();
        }

        /// <summary>
        /// 加载所有的读库类型
        /// </summary>
        static void LoadDbs()
        {
            DbTypes = new List<Type>();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                //if (type.BaseType == typeof(BaseReadDb))
                //{
                //    DbTypes.Add(type);
                //}
            }
        }

        public ConnectionBase GetDbBase()
        {
            int randomIndex = new Random().Next(0, DbTypes.Count);
            var dbType = DbTypes[randomIndex];
            var dbContext = Activator.CreateInstance(dbType) as ConnectionBase;
            return dbContext;
        }
    }
}
