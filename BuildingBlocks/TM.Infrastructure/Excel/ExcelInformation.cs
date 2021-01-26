using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.Excel
{
	/// <summary>
	/// Excel 右击文件 属性信息
	/// </summary>
	public class ExcelInformation
	{
		/// <summary>
		/// 公司
		/// </summary>
		public string Company => "广东土豆分享科技有限公司";
		/// <summary>
		/// 文件主题信息
		/// </summary>
		public string Subject => "土豆导出";
		/// <summary>
		/// 文件作者信息
		/// </summary>
		public string Author => "土豆分享";
		/// <summary>
		/// 作者信息
		/// </summary>
		public string Comments => "土豆分享";
		/// <summary>
		/// xls文件最后保存者信息
		/// </summary>
		public string LastAuthor => "土豆分享";
		/// <summary>
		/// xls文件标题信息
		/// </summary>
		public string Title => "土豆数据";
		/// <summary>
		/// xls文件创建程序信息
		/// </summary>
		public string ApplicationName => "土豆文件程序";
	}
}
