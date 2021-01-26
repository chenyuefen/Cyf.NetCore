using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace TM.Core.Extensions
{
	public static class EnumExtensions
	{
		/// <summary>
		/// 获取枚举DisplayName
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static string GetEnumDisPlayName(this Enum e)
		{
			var enumType = e.GetType();
			var fileName = Enum.GetName(enumType, e);
			var distribute = enumType.GetField(fileName).GetCustomAttributes(typeof(DisplayAttribute), false);
			return distribute.Length > 0 ? ((DisplayAttribute)distribute[0]).Name : null;
		}

		/// <summary>
		/// 根据值得到中文备注
		/// </summary>
		/// <param name="e"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetEnumDesc(Type e, int? value)
		{
			FieldInfo[] fields = e.GetFields()?.Where(x => x.Name != "value__")?.ToArray();
			for (int i = 0, count = fields.Length; i < count; i++)
			{
				if ((int)Enum.Parse(e, fields[i].Name) == value)
				{
					DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (EnumAttributes.Length > 0)
					{
						return EnumAttributes[0].Description;
					}
				}
			}
			return "";
		}

		/// <summary>
		/// 将enum枚举文件转成List<对象>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<EnumberEntity> EnumToList<T>()
		{
			List<EnumberEntity> enumberList = new List<EnumberEntity>();
			foreach (var enumValue in Enum.GetValues(typeof(T)))
			{
				EnumberEntity enumber = new EnumberEntity();
				object[] objArr = enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(EnumAttribute), true);
				if (objArr != null && objArr.Length > 0)
				{
					EnumAttribute enumAttribute = objArr[0] as EnumAttribute;
					enumber.Name = enumAttribute.Name;
					enumber.Desction = enumAttribute.Description;
					enumber.Expand = enumAttribute.Expand;
				}
				enumber.Value = Convert.ToInt32(enumValue);
				enumber.Key = enumValue.ToString();
				enumberList.Add(enumber);
			}
			return enumberList;
		}

	}
}
