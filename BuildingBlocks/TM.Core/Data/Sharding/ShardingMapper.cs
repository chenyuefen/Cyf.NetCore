using System;
using System.ComponentModel.DataAnnotations.Schema;
using TM.Infrastructure.Helpers;

namespace TM.Core.Data.Sharding
{
    /// <summary>
    /// 该方法主要作用在Mysql 和SqlServer切换的时候，表名要求小写敏感
    /// 比如代码逻辑可User配置好对应mysql的user
    /// </summary>
    public static class ShardingMapper
    {
        /// <summary>
        /// 映射物理表
        /// </summary>
        /// <param name="absTable">抽象表类型</param>
        /// <param name="targetTableName">目标物理表名</param>
        /// <param name="targetTableName">所在层集如 "TM.Entity"</param>
        /// <returns></returns>
        public static Type MapTable(Type absTable, string targetTableName, string assemblyName)
        {
            var config = TypeBuilders.GetConfig(absTable);

            //实体必须放到Entity层中,不然会出现莫名调试BUG,原因未知
            config.AssemblyName = assemblyName;
            config.Attributes.RemoveAll(x => x.Attribute == typeof(TableAttribute));
            config.FullName = $"{assemblyName}.{targetTableName}";

            return TypeBuilders.BuildType(config);
        }
    }
}
