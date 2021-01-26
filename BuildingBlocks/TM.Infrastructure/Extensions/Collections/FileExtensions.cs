/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：FileExtensions
// 文件功能描述： 数据转成文件
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System.Data;
using System.IO;
using System.Text;

namespace TM.Infrastructure.Extensions.Collections
{
    /// <summary>
    /// 数据转成文件
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// 将DataTable转换为标准的CSV文件
        /// 以半角逗号（即,）作分隔符，列为空也要表达其存在。
        /// 列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
        /// 列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="fileurl">保存路径</param>
        /// <returns></returns>
        public static void ToCsv(this DataTable table, string fileurl)
        {
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0)
                        sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else
                        sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            File.WriteAllText(fileurl, sb.ToString());
        }
    }
}
