using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TM.Infrastructure.Extensions.Collections
{
    public static class DateTableExtensions
    {
        #region ----将Table数据转化为对象列表----

        /// <summary>
        /// 将Table数据转化为对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<T> DataSetToList<T>(DataSet ds)
        {
            var lstT = new List<T>();

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];

                if (tb.Rows.Count > 0)
                {
                    T t = default(T);

                    t = Activator.CreateInstance<T>(); ////创建指定类型的实例
                    PropertyInfo[] propertyInfo = t.GetType().GetProperties(); //得到类的属性
                    foreach (DataRow row in tb.Rows)
                    {
                        lstT.Add(RetrunObject<T>(tb, propertyInfo, row));
                    }
                }
            }

            return lstT;
        }

        #endregion

        #region ----将Table数据转化为对象列表----

        /// <summary>
        /// 将Table数据转化为对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static T DataSetToObject<T>(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];

                if (tb.Rows.Count > 0)
                {
                    T t = default(T);
                    //Type type = typeof(T);
                    t = Activator.CreateInstance<T>(); ////创建指定类型的实例
                    PropertyInfo[] propertyInfo = t.GetType().GetProperties(); //得到类的属性//type.GetProperties();
                    foreach (DataRow row in tb.Rows)
                    {
                        return RetrunObject<T>(tb, propertyInfo, row);
                    }
                }
            }
            return default(T);
        }

        #endregion

        #region ----将Table数据转化为对象列表----

        /// <summary>
        /// 将Table数据转化为对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tb"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="row"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static T RetrunObject<T>(DataTable tb, PropertyInfo[] propertyInfo, DataRow row)
        {
            T t = Activator.CreateInstance<T>(); ////创建指定类型的实例//(T) type.Assembly.CreateInstance(type.FullName);

            foreach (DataColumn col in tb.Columns)
            {
                var r = propertyInfo.Where(p => p.Name.ToLower() == col.ColumnName.ToLower());
                if (r.Any())
                {
                    PropertyInfo pi = r.First();
                    object obj = (row[col.ColumnName] == DBNull.Value) ? "" : row[col.ColumnName];
                    if (!string.IsNullOrEmpty(obj.ToString()))
                    {
                        if (col.DataType == typeof(DateTime) && pi.PropertyType == typeof(String))
                            obj = ((DateTime)row[col.ColumnName]).ToString();
                        if (col.DataType == typeof(String) && pi.PropertyType == typeof(DateTime))
                        {
                            DateTime temp;
                            obj = DateTime.TryParse(obj.ToString(), out temp);
                        }
                    }
                    //时间类型或者整形,则跳过赋值
                    if (string.IsNullOrEmpty(obj.ToString())
                        && (pi.PropertyType == typeof(DateTime)
                            || pi.PropertyType == typeof(int)
                            || pi.PropertyType == typeof(Int16)
                            || pi.PropertyType == typeof(Int32)
                            || pi.PropertyType == typeof(Int64))) continue;
                    try
                    {
                        pi.SetValue(t, obj, null);
                    }
                    catch
                    {
                    }
                }
            }

            return t;
        }

        #endregion

        #region ----将List数据转换成DataTable----

        /// <summary>
        /// 将List数据转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this IList<T> data, string tableName)
        {
            DataTable table = new DataTable(tableName);

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            var a = prop.GetValue(item);
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        #endregion


        #region----初始化DataTable表结构----

        /// <summary>
        /// 初始化DataTable表结构
        /// </summary>
        /// <typeparam name="T">数据库表实体</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <returns>返回数据库表实体结构的DataTable（无数据）</returns>
        public static DataTable InitDataTable<T>(string tableName)
        {
            DataTable table = new DataTable(tableName);

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");
                table.Columns.Add(dc);
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            return table;
        }

        #endregion

        #region----初始化DataRow结构----

        /// <summary>
        /// 初始化DataRow
        /// </summary>
        /// <typeparam name="T">数据库表实体类</typeparam>
        /// <param name="entity">数据库表实体数据</param>
        /// <param name="table">带有表结构DataTable</param>
        /// <returns>返回数据库表实体结构的DataTable（entity有数据就返回带数据的）</returns>
        public static DataRow InitDataRow<T>(this T entity, DataTable table)
        {
            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                DataRow dr = table.NewRow();
                dr[0] = entity;

                return dr;
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
            {
                try
                {
                    var a = prop.GetValue(entity);
                    row[prop.Name] = prop.GetValue(entity) ?? DBNull.Value;
                }
                catch (Exception ex)
                {
                    row[prop.Name] = DBNull.Value;
                }
            }
            return row;
        }

        #endregion

        #region----将DataTable列名转为属性的DisplayName----

        public static void InitColumnNameAsDisplayName(this DataTable table, PropertyDescriptorCollection properties)
        {
            //修改表列名（先改成别名的话，DataRow会匹配不上的，所以最后再修改别名）
            for (int i = 0; i < table.Columns.Count; i++)
            {
                //一般是对应位置的，所以直接找一次
                if (properties[i].Name == table.Columns[i].ColumnName)
                {
                    table.Columns[i].ColumnName = properties[i].DisplayName;
                    continue;
                }

                //万一是错位的，就循环查找
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name == table.Columns[i].ColumnName)
                    {
                        table.Columns[i].ColumnName = prop.DisplayName;
                        break;
                    }
                }
            }
        }

        #endregion

    }
}
