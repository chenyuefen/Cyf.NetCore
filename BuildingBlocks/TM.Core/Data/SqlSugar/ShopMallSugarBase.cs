using SqlSugar;
using System;
using System.Linq;

namespace TM.Core.Data.SqlSugar
{
    public class SqlServerSugarBase
    {
        private ConnectionConfig _config;

        public SqlServerSugarBase(string connectionString)
        {
            Initialise(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public void Initialise(string connectionString)
        {
            var config = new ConnectionConfig()
            {
                ConnectionString = connectionString,//数据库链接
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            };
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
