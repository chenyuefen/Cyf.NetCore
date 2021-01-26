using System.Data;
using System.Web;
using System.IO;
using System.Text;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using NPOI.XSSF.UserModel;
using System.Threading.Tasks;

namespace TM.Infrastructure.Excel
{
	/// <summary>
	/// Excel导入，导出帮助类
	/// </summary>
	public static class NPOIHelper
	{
		public static MemoryStream ListToExcel<T>(List<T> list, string excelHeadTitle = "")
		{
			HSSFWorkbook workbook = new HSSFWorkbook();
			ExcelInformation information = new ExcelInformation();
			ISheet sheet = workbook.CreateSheet();

			#region 右击文件 属性信息
			{
				DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
				dsi.Company = information.Company;
				workbook.DocumentSummaryInformation = dsi;

				SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
				si.Author = information.Author; //填加xls文件作者信息
				si.ApplicationName = information.ApplicationName; //填加xls文件创建程序信息
				si.LastAuthor = information.LastAuthor; //填加xls文件最后保存者信息
				si.Comments = information.Comments; //填加xls文件作者信息
				si.Title = information.Title; //填加xls文件标题信息
				si.Subject = information.Subject;//填加文件主题信息
				si.CreateDateTime = DateTime.Now;
				workbook.SummaryInformation = si;
			}
			#endregion

			ICellStyle dateStyle = workbook.CreateCellStyle();
			IDataFormat format = workbook.CreateDataFormat();
			dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

			var properties = typeof(T).GetProperties();

			var listCount = list.Count;
			var columnCount = properties.Length;

			//取得列宽
			int[] arrColWidth = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
				var item = properties[i];
				arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(item.Name).Length;
			}

			for (int i = 0; i < listCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					var value = properties[j].GetValue(list[i])?.ToString();
					if (!string.IsNullOrWhiteSpace(value))
					{
						int intTemp = Encoding.GetEncoding(936).GetBytes(value).Length;
						if (intTemp > arrColWidth[j])
						{
							arrColWidth[j] = intTemp;
						}
					}
				}
			}
			int rowIndex = 0;
			foreach (var row in list)
			{
				#region 新建表，填充表头，填充列头，样式
				if (rowIndex == 65535 || rowIndex == 0)
				{
					if (rowIndex != 0)
					{
						sheet = workbook.CreateSheet();
					}

					#region 表头及样式
					if (!string.IsNullOrWhiteSpace(excelHeadTitle))
					{
						IRow headerRow = sheet.CreateRow(0);
						headerRow.HeightInPoints = 25;
						headerRow.CreateCell(0).SetCellValue(excelHeadTitle);

						ICellStyle headStyle = workbook.CreateCellStyle();
						headStyle.Alignment = HorizontalAlignment.Center;
						IFont font = workbook.CreateFont();
						font.FontHeightInPoints = 20;
						font.IsBold = true;
						headStyle.SetFont(font);
						headerRow.GetCell(0).CellStyle = headStyle;

						sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, columnCount - 1));
					}
					#endregion

					#region 列头及样式
					{
						IRow headerRow = string.IsNullOrWhiteSpace(excelHeadTitle) ? sheet.CreateRow(0) : sheet.CreateRow(1);
						ICellStyle headStyle = workbook.CreateCellStyle();
						headStyle.Alignment = HorizontalAlignment.Center;
						IFont font = workbook.CreateFont();
						font.FontHeightInPoints = 10;
						font.IsBold = true;
						//font.Boldweight = 700;
						headStyle.SetFont(font);
						for (int j = 0; j < columnCount; j++)
						{
							var item = properties[j];

							headerRow.CreateCell(j).SetCellValue(item.Name);
							headerRow.GetCell(j).CellStyle = headStyle;

							var arr = (arrColWidth[j] + 1) * 256;
							//限定宽度
							if (arrColWidth[j] > 100) arrColWidth[j] = 100;
							//设置列宽
							sheet.SetColumnWidth(j, (arrColWidth[j] + 1) * 256);
						}
					}
					#endregion

					rowIndex = string.IsNullOrWhiteSpace(excelHeadTitle) ? 1 : 2;
				}
				#endregion

				#region 填充内容
				IRow dataRow = sheet.CreateRow(rowIndex);
				for (int j = 0; j < columnCount; j++)
				{
					var item = properties[j];

					ICell newCell = dataRow.CreateCell(j);
					var columnValue = item.GetValue(row);
					if (columnValue == null)
						continue;

					string drValue = columnValue.ToString();
					if (string.IsNullOrWhiteSpace(drValue))
						continue;

					var propertyType = item.PropertyType;
					if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
						propertyType = propertyType.GetGenericArguments()[0];

					switch (propertyType.BaseType.FullName)
					{
						case "System.Enum"://枚举类型
							int intV = 0;
							int.TryParse(drValue, out intV);
							newCell.SetCellValue(intV);
							continue;
					}

					switch (propertyType.FullName)
					{
						case "System.String"://字符串类型
							newCell.SetCellValue(drValue);
							break;
						case "System.DateTime"://日期类型
							System.DateTime dateV;
							System.DateTime.TryParse(drValue, out dateV);
							newCell.SetCellValue(dateV);

							newCell.CellStyle = dateStyle;//格式化显示
							break;
						case "System.Boolean"://布尔型
							bool boolV = false;
							bool.TryParse(drValue, out boolV);
							newCell.SetCellValue(boolV);
							break;
						case "System.Int16"://整型
						case "System.Int32":
						case "System.Int64":
						case "System.Byte":
							//long intV = 0;
							//long.TryParse(drValue, out intV);
							newCell.SetCellValue(drValue);
							break;
						case "System.Decimal"://浮点型
						case "System.Double":
							double doubV = 0;
							double.TryParse(drValue, out doubV);
							newCell.SetCellValue(doubV);
							break;
						case "System.DBNull"://空值处理
							newCell.SetCellValue("");
							break;
						default:
							newCell.SetCellValue("");
							break;
					}
				}
				#endregion

				rowIndex++;
			}
			using (MemoryStream ms = new MemoryStream())
			{
				workbook.Write(ms);
				ms.Flush();
				ms.Position = 0;
				//sheet.Dispose();
				return ms;
			}
		}

		public static async Task<DataTable> ExcelToDTAsync(IFormFileCollection formFiles)
		{
			foreach (var file in formFiles)
			{
				DataTable dt = new DataTable();
				var fileName = file.FileName;
				if (!fileName.Contains(".xls") && !fileName.Contains("xlsx"))
				{
					continue;
				}
				using var ms = new MemoryStream();
				await file.CopyToAsync(ms);
				ms.Position = 0;
				var iWorkbook = default(IWorkbook);
				if (fileName.Split('.')[1] == "xls")
				{
					iWorkbook = new HSSFWorkbook(ms);
				}
				else if (fileName.Split('.')[1] == "xlsx")
				{
					iWorkbook = new XSSFWorkbook(ms);
				}

				ISheet sheet = iWorkbook.GetSheetAt(0);
				IEnumerator rows = sheet.GetRowEnumerator();

				IRow headerRow = sheet.GetRow(0);
				int cellCount = headerRow.LastCellNum;

				for (int j = 0; j < cellCount; j++)
				{
					ICell cell = headerRow.GetCell(j);
					dt.Columns.Add(cell.ToString());
				}

				for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
				{
					IRow row = sheet.GetRow(i);
					DataRow dataRow = dt.NewRow();

					for (int j = row.FirstCellNum; j < cellCount; j++)
					{
						if (row.GetCell(j) != null)
							dataRow[j] = row.GetCell(j).ToString().Trim();
					}

					dt.Rows.Add(dataRow);
				}
				return dt;
			}
			return null;
		}

		#region Excel导出方法

		/// <summary>
		/// Excel导出（服务项目调用）
		/// </summary>
		/// <param name="dtSource">DataTable数据源</param>
		/// <param name="filePath">文件路径</param>
		/// <param name="excelHeadTitle">文件第一行标题</param>
		public static byte[] Export<T>(List<T> entitys, string excelHeadTitle = "", bool IsList = false)
		{
			DataTable dtSource = new DataTable();
			if (IsList)
				dtSource = entitys.IListToDataTable();
			else
				dtSource = entitys.ListToDataTable();
			using (MemoryStream ms = GetExcelStream(dtSource, excelHeadTitle))
			{
				return ms.GetBuffer();
			}
		}

		/// <summary>
		/// Excel导出（服务项目调用）
		/// </summary>
		/// <param name="dtSource">DataTable数据源</param>
		/// <param name="filePath">文件路径</param>
		/// <param name="excelHeadTitle">文件第一行标题</param>
		public static void Export(DataTable dtSource, string filePath, string excelHeadTitle = "")
		{
			using (MemoryStream ms = GetExcelStream(dtSource, excelHeadTitle))
			{
				using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				{
					byte[] data = ms.ToArray();
					fs.Write(data, 0, data.Length);
					fs.Flush();
				}
			}
		}

		/// <summary>
		/// Excel导出（服务项目调用）
		/// </summary>
		/// <param name="dtSource">DataTable数据源</param>
		/// <param name="filePath">文件路径</param>
		/// <param name="excelHeadTitle">文件第一行标题</param>
		public static void Export(DataTable dtSource, string fileDir, string fileName, string excelHeadTitle)
		{
			var filePath = fileDir + fileName;
			Export(dtSource, filePath, excelHeadTitle);
		}

		#endregion

		#region Excel导出文件流 GetExcelStream
		/// <summary>
		/// Excel导出文件流（服务项目调用）
		/// </summary>
		/// <param name="dtSource">DataTable数据源</param>
		/// <param name="excelHeadTitle">文件第一行标题</param>
		public static MemoryStream GetExcelStream(DataTable dtSource, string excelHeadTitle = "")
		{
			HSSFWorkbook workbook = new HSSFWorkbook();
			ExcelInformation information = new ExcelInformation();
			ISheet sheet = workbook.CreateSheet();

			#region 右击文件 属性信息
			{
				DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
				dsi.Company = information.Company;
				workbook.DocumentSummaryInformation = dsi;

				SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
				si.Author = information.Author; //填加xls文件作者信息
				si.ApplicationName = information.ApplicationName; //填加xls文件创建程序信息
				si.LastAuthor = information.LastAuthor; //填加xls文件最后保存者信息
				si.Comments = information.Comments; //填加xls文件作者信息
				si.Title = information.Title; //填加xls文件标题信息
				si.Subject = information.Subject;//填加文件主题信息
				si.CreateDateTime = DateTime.Now;
				workbook.SummaryInformation = si;
			}
			#endregion

			ICellStyle dateStyle = workbook.CreateCellStyle();
			IDataFormat format = workbook.CreateDataFormat();
			dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

			//取得列宽
			int[] arrColWidth = new int[dtSource.Columns.Count];
			foreach (DataColumn item in dtSource.Columns)
			{
				arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
			}
			for (int i = 0; i < dtSource.Rows.Count; i++)
			{
				for (int j = 0; j < dtSource.Columns.Count; j++)
				{
					int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
					if (intTemp > arrColWidth[j])
					{
						arrColWidth[j] = intTemp;
					}
				}
			}
			int rowIndex = 0;
			foreach (DataRow row in dtSource.Rows)
			{
				#region 新建表，填充表头，填充列头，样式
				if (rowIndex == 65535 || rowIndex == 0)
				{
					if (rowIndex != 0)
					{
						sheet = workbook.CreateSheet();
					}

					#region 表头及样式
					if (!string.IsNullOrWhiteSpace(excelHeadTitle))
					{
						IRow headerRow = sheet.CreateRow(0);
						headerRow.HeightInPoints = 25;
						headerRow.CreateCell(0).SetCellValue(excelHeadTitle);

						ICellStyle headStyle = workbook.CreateCellStyle();
						headStyle.Alignment = HorizontalAlignment.Center;
						IFont font = workbook.CreateFont();
						font.FontHeightInPoints = 20;
						font.IsBold = true;
						headStyle.SetFont(font);
						headerRow.GetCell(0).CellStyle = headStyle;

						sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
					}
					#endregion

					#region 列头及样式
					{
						IRow headerRow = string.IsNullOrWhiteSpace(excelHeadTitle) ? sheet.CreateRow(0) : sheet.CreateRow(1);
						ICellStyle headStyle = workbook.CreateCellStyle();
						headStyle.Alignment = HorizontalAlignment.Center;
						IFont font = workbook.CreateFont();
						font.FontHeightInPoints = 10;
						font.IsBold = true;
						//font.Boldweight = 700;
						headStyle.SetFont(font);
						foreach (DataColumn column in dtSource.Columns)
						{
							headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
							headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

							var arr = (arrColWidth[column.Ordinal] + 1) * 256;
							//限定宽度
							if (arrColWidth[column.Ordinal] > 100) arrColWidth[column.Ordinal] = 100;
							//设置列宽
							sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
						}
					}
					#endregion

					rowIndex = string.IsNullOrWhiteSpace(excelHeadTitle) ? 1 : 2;
				}
				#endregion

				#region 填充内容
				IRow dataRow = sheet.CreateRow(rowIndex);
				foreach (DataColumn column in dtSource.Columns)
				{
					ICell newCell = dataRow.CreateCell(column.Ordinal);

					string drValue = row[column].ToString();

					switch (column.DataType.BaseType.FullName)
					{
						case "System.Enum"://枚举类型
							int intV = 0;
							int.TryParse(drValue, out intV);
							newCell.SetCellValue(intV);
							continue;
					}

					switch (column.DataType.ToString())
					{
						case "System.String"://字符串类型
							newCell.SetCellValue(drValue);
							break;
						case "System.DateTime"://日期类型
							System.DateTime dateV;
							System.DateTime.TryParse(drValue, out dateV);
							newCell.SetCellValue(dateV);

							newCell.CellStyle = dateStyle;//格式化显示
							break;
						case "System.Boolean"://布尔型
							bool boolV = false;
							bool.TryParse(drValue, out boolV);
							newCell.SetCellValue(boolV);
							break;
						case "System.Int16"://整型
						case "System.Int32":
						case "System.Int64":
						case "System.Byte":
							//long intV = 0;
							//long.TryParse(drValue, out intV);
							newCell.SetCellValue(drValue);
							break;
						case "System.Decimal"://浮点型
						case "System.Double":
							double doubV = 0;
							double.TryParse(drValue, out doubV);
							newCell.SetCellValue(doubV);
							break;
						case "System.DBNull"://空值处理
							newCell.SetCellValue("");
							break;
						default:
							newCell.SetCellValue("");
							break;
					}
				}
				#endregion

				rowIndex++;
			}
			using (MemoryStream ms = new MemoryStream())
			{
				workbook.Write(ms);
				ms.Flush();
				ms.Position = 0;
				//sheet.Dispose();
				return ms;
			}
		}
		#endregion

		#region 读取Excel ,默认第一行为标头 Import

		public static async Task<List<T>> ImportAsync<T>(IFormFileCollection formFiles) where T : class, new()
		{
			List<T> list = new List<T>();
			foreach (var file in formFiles)
			{
				DataTable dt = new DataTable();
				var fileName = file.FileName;
				if (!fileName.Contains(".xls") && !fileName.Contains("xlsx"))
				{
					continue;
				}
				using var ms = new MemoryStream();
				await file.CopyToAsync(ms);
				ms.Position = 0;
				var iWorkbook = default(IWorkbook);
				if (fileName.Split('.')[1] == "xls")
				{
					iWorkbook = new HSSFWorkbook(ms);
				}
				else if (fileName.Split('.')[1] == "xlsx")
				{
					iWorkbook = new XSSFWorkbook(ms);
				}

				ISheet sheet = iWorkbook.GetSheetAt(0);
				IEnumerator rows = sheet.GetRowEnumerator();

				IRow headerRow = sheet.GetRow(0);
				int cellCount = headerRow.LastCellNum;

				for (int j = 0; j < cellCount; j++)
				{
					ICell cell = headerRow.GetCell(j);
					dt.Columns.Add(cell.ToString());
				}

				for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
				{
					IRow row = sheet.GetRow(i);
					DataRow dataRow = dt.NewRow();

					for (int j = row.FirstCellNum; j < cellCount; j++)
					{
						if (row.GetCell(j) != null)
							dataRow[j] = row.GetCell(j).ToString().Trim();
					}

					dt.Rows.Add(dataRow);
				}
				var dtList = dt.DataTableToList<T>();
				if (dtList?.Count > 0)
					list.AddRange(dtList);
			}
			return list;
		}

		/// <summary>
		/// 读取excel ,默认第一行为标头
		/// </summary>
		/// <param name="filePath">excel文档路径</param>
		/// <returns></returns>
		public static DataTable Import(string filePath)
		{
			DataTable dt = new DataTable();

			var iWorkbook = default(IWorkbook);
			using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				if (Path.GetExtension(filePath) == ".xls")
					iWorkbook = new HSSFWorkbook(stream);
				else
					iWorkbook = new XSSFWorkbook(stream);
			}
			ISheet sheet = iWorkbook.GetSheetAt(0);
			IEnumerator rows = sheet.GetRowEnumerator();

			IRow headerRow = sheet.GetRow(0);
			int cellCount = headerRow.LastCellNum;

			for (int j = 0; j < cellCount; j++)
			{
				ICell cell = headerRow.GetCell(j);
				dt.Columns.Add(cell.ToString());
			}

			for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
			{
				IRow row = sheet.GetRow(i);
				DataRow dataRow = dt.NewRow();

				for (int j = row.FirstCellNum; j < cellCount; j++)
				{
					if (row.GetCell(j) != null)
						dataRow[j] = row.GetCell(j).ToString();
				}

				dt.Rows.Add(dataRow);
			}
			return dt;
		}
		#endregion

		#region List转换为DataTable ListToDataTable<T>
		/// <summary>
		/// List转换为DataTable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static DataTable ListToDataTable<T>(this IEnumerable<T> list)
		{

			//创建属性的集合    
			List<PropertyInfo> pList = new List<PropertyInfo>();
			//获得反射的入口    

			Type type = typeof(T);
			DataTable dt = new DataTable();
			Dictionary<string, string> map = new Dictionary<string, string>();
			//把所有的public属性加入到集合 并添加DataTable的列    
			System.Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
			{
				pList.Add(p);
				var arrDesc = p.GetCustomAttribute(typeof(DescriptionAttribute));
				map.Add(p.Name, ((DescriptionAttribute)arrDesc).Description);
				dt.Columns.Add(map[p.Name], p.PropertyType);
			});
			foreach (var item in list)
			{
				//创建一个DataRow实例    
				DataRow row = dt.NewRow();
				//给row 赋值    
				pList.ForEach(p => row[map[p.Name]] = p.GetValue(item, null));
				//加入到DataTable    
				dt.Rows.Add(row);
			}
			return dt;
		}
		#endregion

		#region DataTable转换为List DataTableToList
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static List<T> DataTableToList<T>(this DataTable dt) where T : class, new()
		{
			//创建一个属性的列表    
			List<PropertyInfo> prlist = new List<PropertyInfo>();
			//获取TResult的类型实例  反射的入口    

			Type t = typeof(T);
			Dictionary<string, string> map = new Dictionary<string, string>();

			//获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
			System.Array.ForEach<PropertyInfo>(t.GetProperties(), p =>
			{
				var arrDesc = ((DescriptionAttribute)p.GetCustomAttribute(typeof(DescriptionAttribute))).Description;
				if (string.IsNullOrEmpty(arrDesc))
					arrDesc = p.Name;

				map.Add(p.Name, arrDesc);
				if (dt.Columns.IndexOf(arrDesc) != -1)
					prlist.Add(p);
			});

			//System.Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

			//创建返回的集合    

			List<T> oblist = new List<T>();

			foreach (DataRow row in dt.Rows)
			{
				//创建TResult的实例    
				T ob = new T();
				var isTransSuccess = true;
				//找到对应的数据  并赋值    
				foreach (var p in prlist)
				{
					if (row[map[p.Name]] != DBNull.Value)
					{
						var drValue = row[map[p.Name]].ToString();
						switch (p.PropertyType.BaseType.FullName)
						{
							case "System.Enum"://枚举类型
								int intV = 0;
								isTransSuccess = int.TryParse(drValue, out intV);
								p.SetValue(ob, intV, null);
								break;
						}

						switch (p.PropertyType.FullName)
						{
							case "System.String"://字符串类型
								p.SetValue(ob, drValue, null);
								break;
							case "System.DateTime"://日期类型
								DateTime dateV;
								isTransSuccess = DateTime.TryParse(drValue, out dateV);
								p.SetValue(ob, dateV, null);
								break;
							case "System.Boolean"://布尔型
								bool boolV = false;
								isTransSuccess = bool.TryParse(drValue, out boolV);
								p.SetValue(ob, boolV, null);
								break;
							case "System.Int16"://整型
							case "System.Int32":
							case "System.Int64":
							case "System.Byte":
								long intV = 0;
								isTransSuccess = long.TryParse(drValue, out intV);
								p.SetValue(ob, intV, null);
								break;
							case "System.Decimal"://浮点型
							case "System.Double":
								double doubV = 0;
								isTransSuccess = double.TryParse(drValue, out doubV);
								p.SetValue(ob, doubV, null);
								break;
							case "System.DBNull"://空值处理
								p.SetValue(ob, "", null);
								break;
							default:
								p.SetValue(ob, "", null);
								isTransSuccess = false;
								break;
						}

						if (!isTransSuccess)
						{
							break;
						}
					}
				}

				//放入到返回的集合中.   
				if (isTransSuccess)
				{
					oblist.Add(ob);
				}
			}
			return oblist;
		}
		#endregion

		#region 将集合类转换成DataTable IListToDataTable
		/// <summary>
		/// 将集合类转换成DataTable
		/// </summary>
		/// <param name="list">集合</param>
		/// <returns></returns>
		public static DataTable IListToDataTable(this IList list)
		{
			DataTable result = new DataTable();
			if (list.Count > 0)
			{
				PropertyInfo[] propertys = list[0].GetType().GetProperties();

				foreach (PropertyInfo pi in propertys)
				{
					var propertyType = pi.PropertyType;
					if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
						propertyType = propertyType.GetGenericArguments()[0];

					result.Columns.Add(pi.Name, propertyType);
				}
				for (int i = 0; i < list.Count; i++)
				{
					ArrayList tempList = new ArrayList();
					foreach (PropertyInfo pi in propertys)
					{
						object obj = pi.GetValue(list[i], null);
						tempList.Add(obj);
					}
					object[] array = tempList.ToArray();
					result.LoadDataRow(array, true);
				}
			}
			return result;
		}
		#endregion

		#region 将泛型集合类转换成DataTable IListToDataTable<T>
		/// <summary>    
		/// 将泛型集合类转换成DataTable
		/// </summary>    
		/// <typeparam name="T">集合项类型</typeparam>
		/// <param name="list">集合</param>    
		/// <returns>数据集(表)</returns>    
		public static DataTable IListToDataTable<T>(IList<T> list)
		{
			return IListToDataTable<T>(list, null);

		}

		/// <summary>    
		/// 将泛型集合类转换成DataTable    
		/// </summary>    
		/// <typeparam name="T">集合项类型</typeparam>    
		/// <param name="list">集合</param>    
		/// <param name="propertyName">需要返回的列的列名</param>    
		/// <returns>数据集(表)</returns>    
		public static DataTable IListToDataTable<T>(IList<T> list, params string[] propertyName)
		{
			List<string> propertyNameList = new List<string>();
			if (propertyName != null)
				propertyNameList.AddRange(propertyName);
			DataTable dataTable = new DataTable();
			if (list.Count == 0) return dataTable;

			PropertyInfo[] propertys = list[0].GetType().GetProperties();
			foreach (PropertyInfo property in propertys)
			{
				if (propertyNameList.Count == 0)
				{
					dataTable.Columns.Add(property.Name, property.PropertyType);
				}
				else
				{
					if (propertyNameList.Contains(property.Name))
						dataTable.Columns.Add(property.Name, property.PropertyType);
				}
			}

			for (int i = 0; i < list.Count; i++)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PropertyInfo pi in propertys)
				{
					if (propertyNameList.Count == 0)
					{
						object obj = pi.GetValue(list[i], null);
						arrayList.Add(obj);
					}
					else
					{
						if (propertyNameList.Contains(pi.Name))
						{
							object obj = pi.GetValue(list[i], null);
							arrayList.Add(obj);
						}
					}
				}
				object[] array = arrayList.ToArray();
				dataTable.LoadDataRow(array, true);
			}
			return dataTable;
		}
		#endregion
	}
}
