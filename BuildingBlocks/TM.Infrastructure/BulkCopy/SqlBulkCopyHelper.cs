/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：SqlBulkCopyHelper
// 文件功能描述： SqlServer帮助类
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TM.Infrastructure.BulkCopy
{
    /// <summary>
    /// 数据批量导入SqlServer
    /// </summary>
    public static class SqlBulkCopyHelper
    {
        #region ----同步方法----

        #region ----批量插入(SqlBulkCopy)----

        /// <summary>
        /// 泛型集合批量插入数据库
        /// 如果是同表多发执行，需要加锁
        /// </summary>
        /// <typeparam name="T">泛型集合的类型</typeparam>
        /// <param name="conn">连接对象</param>
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>
        /// <param name="list">要插入大泛型集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static bool Insert<T>(SqlConnection conn, string tableName, IList<T> list, string where = null) where T : class, new()
        {
            var table = new DataTable();
            try
            {
                var sqlCommand = new SqlCommand($"SELECT COUNT(*) FROM {tableName} {where}", conn);
                var countStart = Convert.ToInt32(sqlCommand.ExecuteScalar());

                using (var bulkCopy = new SqlBulkCopy(conn))
                {
                    var props = TypeDescriptor.GetProperties(typeof(T))
                        .Cast<PropertyDescriptor>()
                        .Where(item => item.PropertyType.Namespace.Equals("System"))
                        .ToArray();

                    foreach (var item in props)
                    {
                        bulkCopy.ColumnMappings.Add(item.Name, item.Name);
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
                    bulkCopy.BatchSize = list.Count;
                    bulkCopy.DestinationTableName = tableName;
                    WriteToServer(bulkCopy, table);
                }
                var countEnd = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (countEnd - countStart == list.Count)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                table.Dispose();
            }
        }

        /// <summary>
        /// 执行DataTable对象写入数据库
        /// 如果出现异常，SqlBulkCopy 会使数据库回滚，所有Table中的记录都不会插入到数据库中，
        /// 此时，把Table折半插入，先插入一半，再插入一半。如此递归，直到只有一行时，如果插入异常，则返回。
        /// </summary>
        /// <param name="bulkCopy"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static void WriteToServer(SqlBulkCopy bulkCopy, DataTable table)
        {
            try
            {
                bulkCopy.WriteToServer(table);
                return;
            }
            catch (Exception ex)
            {
                if (table.Rows.Count == 1)
                {
                    return;
                }
                int middle = table.Rows.Count / 2;

                DataTable tempTable = table.Clone();
                for (int i = 0; i < middle; i++)
                    tempTable.ImportRow(table.Rows[i]);
                WriteToServer(bulkCopy, tempTable);

                tempTable.Clear();
                for (int i = middle; i < table.Rows.Count; i++)
                    tempTable.ImportRow(table.Rows[i]);
                WriteToServer(bulkCopy, tempTable);
            }
        }

        #endregion

        #region ----批量更新(SqlDataAdapter)----

        /// <summary>
        /// 泛型集合批量更新数据库
        /// </summary>
        /// <typeparam name="T">泛型集合的类型</typeparam>
        /// <param name="conn">连接对象</param>
        /// <param name="columns">更新的字段 ','拼接</param>
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>
        /// <param name="list">要插入大泛型集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static bool Update<T>(SqlConnection conn, string columns, string tableName, IList<T> list, string where = null) where T : class, new()
        {
            var table = new DataTable();
            SqlTransaction trans = null;
            try
            {
                trans = conn.BeginTransaction();
                var sql = $"SELECT {columns} FROM {tableName} WHERE 1=0"; //WHERE=false 保证查出是一个空表
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
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

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())  //核心对象
                    {
                        dataAdapter.SelectCommand = new SqlCommand(sql, conn);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                        commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                        commandBuilder.SetAllValues = true;
                        foreach (DataRow dataRow in table.Rows)
                        {
                            if (dataRow.RowState == DataRowState.Unchanged)
                            {
                                dataRow.SetModified();
                            }
                            dataAdapter.Update(table);
                            table.AcceptChanges();  //提交
                        }
                    }
                }
                trans.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                table.Dispose();
            }
        }

        #endregion

        #endregion

        #region ----异步方法----

        #region ----批量插入(SqlBulkCopy)----

        /// <summary>
        /// 泛型集合批量插入数据库
        /// </summary>
        /// <typeparam name="T">泛型集合的类型</typeparam>
        /// <param name="conn">连接对象</param>
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>
        /// <param name="list">要插入大泛型集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static async Task<bool> InsertAsync<T>(SqlConnection conn, string tableName, IList<T> list, string where = null) where T : class, new()
        {
            var table = new DataTable();
            try
            {
                var sqlCommand = new SqlCommand($"SELECT COUNT(*) FROM {tableName} {where}", conn);
                var countStart = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());

                using (var bulkCopy = new SqlBulkCopy(conn))
                {
                    var props = TypeDescriptor.GetProperties(typeof(T))
                        .Cast<PropertyDescriptor>()
                        .Where(item => item.PropertyType.Namespace.Equals("System"))
                        .ToArray();

                    foreach (var item in props)
                    {
                        bulkCopy.ColumnMappings.Add(item.Name, item.Name);
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
                    bulkCopy.BatchSize = list.Count;
                    bulkCopy.DestinationTableName = tableName;
                    await WriteToServerAsync(bulkCopy, table);
                }
                var countEnd = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());
                if (countEnd - countStart == list.Count)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                table.Dispose();
            }
        }

        /// <summary>
        /// 执行DataTable对象写入数据库
        /// 如果出现异常，SqlBulkCopy 会使数据库回滚，所有Table中的记录都不会插入到数据库中，
        /// 此时，把Table折半插入，先插入一半，再插入一半。如此递归，直到只有一行时，如果插入异常，则返回。
        /// </summary>
        /// <param name="bulkCopy"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static async Task WriteToServerAsync(SqlBulkCopy bulkCopy, DataTable table)
        {
            try
            {
                await bulkCopy.WriteToServerAsync(table);
                return;
            }
            catch (Exception ex)
            {
                if (table.Rows.Count == 1)
                {
                    return;
                }
                int middle = table.Rows.Count / 2;

                DataTable tempTable = table.Clone();
                for (int i = 0; i < middle; i++)
                    tempTable.ImportRow(table.Rows[i]);
                await WriteToServerAsync(bulkCopy, tempTable);

                tempTable.Clear();
                for (int i = middle; i < table.Rows.Count; i++)
                    tempTable.ImportRow(table.Rows[i]);
                await WriteToServerAsync(bulkCopy, tempTable);
            }
        }

        #endregion

        #endregion
    }
}
