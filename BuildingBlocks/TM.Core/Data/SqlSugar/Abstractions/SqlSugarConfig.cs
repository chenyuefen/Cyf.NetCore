using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using TM.Core.Data.SqlSugar.Abstractions;
using TM.Infrastructure.Configs;
using TM.Infrastructure.Extensions.Common;

namespace TM.Core.Data.SqlSugar
{
    public static class SqlSugarConfig
    {
        public static List<SqlFuncExternal> GetLambda()
        {
            //Lambda自定义解析
            var expMethods = new List<SqlFuncExternal>
                        {
                            new SqlFuncExternal()
                            {
                                UniqueMethodName = "ToDateFormat",
                                MethodValue = (expInfo, dbType, expContext) =>
                                {
                                    switch (dbType)
                                    {
                                        case DbType.SqlServer:
                                            return $"CONVERT (VARCHAR (10), {expInfo.Args[0].MemberName}, 121 )";

                                        case DbType.MySql:
                                            return $"DATE_FORMAT( {expInfo.Args[0].MemberName}, '%Y-%m-%d' ) ";

                                        case DbType.Sqlite:
                                            return $"date({expInfo.Args[0].MemberName})";

                                        case DbType.PostgreSQL:
                                        case DbType.Oracle:
                                            return $"to_date({expInfo.Args[0].MemberName},yyyy-MM-dd)";

                                        default:
                                            throw new Exception("未实现");
                                    }
                                }
                            },
                        };
            return expMethods;
        }

        /// <summary>
        /// 默认是SqlServer
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static ConnectionConfig GetConnectionString(IConfiguration configuration)
        {

            //var j= configuration.GetSection("Authentication:Jwt").Get<Jwt>();

            var master = configuration.GetSection("DataBase_Default").GetSection("ConnectionStrings")
                .GetSection("Master").Value;

            var dbType = configuration.GetSection("DataBase_Default").GetSection("DbType").Value;

            var slaveSection = configuration.GetSection("DataBase_Default").GetSection("ConnectionStrings").GetSection("Slave");
            var slaveConnectionConfigs = slaveSection.Get<List<SlaveConnectionConfig>>();



            //var dbConfig = dbSection.Get<DataBaseConfig>();


            var dbConfig2 = ConfigHelper.GetAppSettings<DataBaseConfig>("appsettings.json", "", "DataBase_Default:ConnectionStrings");

            var dbConfig3 = ConfigHelper.GetJsonConfig("appsettings.json", "").GetSection("DataBase_Default:ConnectionStrings")
                .Get<DataBaseConfig>();

            //var dbConfig= masterJson.ToObject<DataBaseConfig>();
            var cfg = new ConnectionConfig()
            {
                ConnectionString = master,
                DbType = dbType.ToEnum<DbType>(),
                IsAutoCloseConnection = true,       //默认false,自动关闭数据库连接, 设置为true无需使用using或者Close操作
                InitKeyType = InitKeyType.Attribute,    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                IsShardSameThread = true, //是否共享线程
                SlaveConnectionConfigs = slaveConnectionConfigs // slaveConnectionConfigs.MapTo<List<SlaveConnectionConfig>>() //支持多个从库
                //new List<SlaveConnectionConfig>() {//从连接
                // new SlaveConnectionConfig() { HitRate=10, ConnectionString=_config.ConnectionString2 },
                // new SlaveConnectionConfig() { HitRate=30, ConnectionString=_config.ConnectionString3 }
                //} 
            };
            return cfg;

        }


        /// <summary>
        /// 解决数据库表名与实体名称不一致的问题 别名表
        /// </summary>
        public static readonly MappingTableList listTable = new MappingTableList()
        {
           new MappingTable() { EntityName="SuperGoUsers",DbTableName="supergo_user",DbShortTaleName="user"},
        };

        public static readonly MappingColumnList columns = new MappingColumnList()
        {
        };

        public static ConnectionConfig GetConnectionString(string configFileName, string basePath)
        {
            var dbConfig = ConfigHelper.GetAppSettings<DataBaseConfig>(configFileName, "", basePath);
            var cfg = new ConnectionConfig()
            {
                ConnectionString = dbConfig.ConnectionStrings.Master,
                DbType = dbConfig.DbType.ToEnum<DbType>(),
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            };
            return cfg;
        }
    }
}
