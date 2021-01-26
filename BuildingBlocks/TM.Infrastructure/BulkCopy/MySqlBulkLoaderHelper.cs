/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：MySqlBulkLoaderHelper
// 文件功能描述： MySql帮助类
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TM.Infrastructure.Configs;
using TM.Infrastructure.Extensions.Collections;

namespace TM.Infrastructure.BulkCopy
{
    /// <summary>
    /// 数据批量导入MySql
    /// </summary>
    public static class MySqlBulkLoaderHelper
    {
        /// <summary>
        /// mysql链接
        /// </summary>
        private static string MY_SQL_CONNECTION = ConfigHelper.GetJsonConfig("appsettings.json").GetSection("MySqlConnection").Value;

        /// <summary>
        /// 插入mysql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<bool> InsertAsync<T>(string tableName, IList<T> list) where T : class, new()
        {
            using (MySqlConnection connection = new MySqlConnection(MY_SQL_CONNECTION))
            {
                var table = new DataTable() { TableName = tableName };
                MySqlTransaction sqlTransaction = null;
                try
                {
                    connection.Open();
                    sqlTransaction = connection.BeginTransaction();

                    var props = TypeDescriptor.GetProperties(typeof(T))
                        .Cast<PropertyDescriptor>()
                        .Where(item => item.PropertyType.Namespace.Equals("System"))
                        .ToArray();

                    foreach (var item in props)
                    {
                        table.Columns.Add(item.Name, Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
                    }

                    var values = new object[props.Length];
                    foreach (var item in list)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                        table.Rows.Add(values);
                    }
                    var fileurl = Path.Combine(Directory.GetCurrentDirectory(), $"Resources/Csv/{DateTime.Now.ToString("D")}/{table.TableName}-{DateTime.Now.ToFileTimeUtc().ToString()}.csv");
                    table.ToCsv(fileurl);

                    var columns = table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(connection)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = fileurl,
                        NumberOfLinesToSkip = 0,
                        TableName = table.TableName,
                    };
                    bulk.Columns.AddRange(columns);

                    int result = await bulk.LoadAsync();  //https://www.cnblogs.com/doublesnow/p/10562215.html
                    if (result == list.Count)
                    {
                        sqlTransaction.Rollback();
                        return false;
                    }
                    sqlTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                    }
                    return false;
                }
            }
        }
    }
}
