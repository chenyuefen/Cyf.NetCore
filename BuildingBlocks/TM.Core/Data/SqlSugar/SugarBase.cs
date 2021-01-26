using SqlSugar;
using System;
using System.Linq;

namespace TM.Core.Data.SqlSugar
{
    /// <summary>
    /// sugar支持读写分离
    /// </summary>
    public class SugarBase
    {
        private ConnectionConfig _config;

        public SugarBase(ConnectionConfig config)
        {
            Initialise(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void Initialise(ConnectionConfig config = null)
        {

            _config = config ?? throw new ArgumentNullException(nameof(config));
            try
            {
                Db = new SqlSugarClient(_config)
                {
                    MappingTables = mapTableList,      //别名表
                    IgnoreColumns = mapIgnoreColumnList,     //过滤列
                    MappingColumns = mapColumnList,    //别名列
                    IgnoreInsertColumns = ignoreInsertList
                };
                //开启日志
                Db.Ado.IsEnableLogEvent = true;
                //SQL执行完事件
                Db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    Console.WriteLine();
                };
                //SQL执行前事件
                Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                };
                Db.Aop.OnError = (exp) =>//执行SQL 错误事件
                {
                    string ss = exp.Message;
                };
            }
            catch (Exception ex)
            {
                throw new Exception("连接数据库出错，请检查您的连接字符串和网络。 ex:" + ex.Message);
            }

            //var conn = new ConnectionConfig()
            //{
            //    ConnectionString = _config.ConnectionString,
            //    DbType = _config.DbType,
            //    IsAutoCloseConnection = _config.IsAutoCloseConnection,       //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
            //    InitKeyType = _config.InitKeyType,    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
            //    IsShardSameThread = _config.IsShardSameThread, //是否共享线程
            //    SlaveConnectionConfigs = _config.SlaveConnectionConfigs //支持多个从库

            //    //new List<SlaveConnectionConfig>() {//从连接
            //    // new SlaveConnectionConfig() { HitRate=10, ConnectionString=_config.ConnectionString2 },
            //    // new SlaveConnectionConfig() { HitRate=30, ConnectionString=_config.ConnectionString3 }
            //    //} 
            //};
        }


        // public SimpleClient<Test> TestDb { get; set; }

        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        /// <summary>
        /// 别名列
        /// </summary>
        private static readonly IgnoreColumnList mapIgnoreColumnList = new IgnoreColumnList()
        {
           new IgnoreColumn(){EntityName = "",PropertyName =""}
        };

        /// <summary>
        /// 解决数据库表名与实体名称不一致的问题 别名表
        /// </summary>
        private static readonly MappingTableList mapTableList = new MappingTableList()
        {
           new MappingTable() { EntityName="SystemUser",DbTableName="SystemUser",DbShortTaleName=""}
        };

        /// <summary>
        ///解决数据库列名与实体属性名称不一致的问题 别名列
        /// </summary>
        private static readonly MappingColumnList mapColumnList = new MappingColumnList()
        {
           new MappingColumn() {PropertyName="实体类属性名称",DbColumnName="数据库表里面的列名",EntityName="实体类名称"}
        };

        /// <summary>
        ///过滤列
        /// </summary>
        private static readonly IgnoreColumnList ignoreInsertList = new IgnoreColumnList()
        {
           new IgnoreColumn() { EntityName="ID",PropertyName="SystemUser"}
        };
    }
}
