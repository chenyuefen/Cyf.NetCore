﻿using System;
using System.IO;
using TM.Infrastructure.Extensions.Common;

// ReSharper disable once CheckNamespace
namespace TM.Infrastructure.Extensions
{
    /// <summary>
    /// 对象(<see cref="object"/>) 扩展
    /// </summary>
    public static class ObjectExtensions
    {
        #region DeepClone(对象深拷贝)

        /// <summary>
        /// 对象深度拷贝，复制相同数据，但指向内存位置不一样的数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">值</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
            {
                return default(T);
            }

            if (!typeof(T).HasAttribute<SerializableAttribute>(true))
            {
                throw new NotSupportedException($"当前对象未标记特性“{typeof(SerializableAttribute)}”，无法进行DeepClone操作");
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }

        #endregion

        #region ToDynamic(将对象转换为dynamic)

        ///// <summary>
        ///// 将对象[主要是匿名对象]转换为dynamic
        ///// </summary>
        ///// <param name="value">值</param>
        ///// <returns></returns>
        //public static dynamic ToDynamic(this object value)
        //{
        //    IDictionary<string,object> expando=new ExpandoObject();
        //    Type type = value.GetType();
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
        //    foreach (PropertyDescriptor property in properties)
        //    {
        //        var val = property.GetValue(value);
        //        if (property.PropertyType.FullName != null &&
        //            property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
        //        {
        //            dynamic dval = val.ToDynamic();
        //            expando.Add(property.Name,dval);
        //        }
        //        else
        //        {
        //            expando.Add(property.Name, val);
        //        }
        //    }

        //    return (ExpandoObject) expando;
        //}

        #endregion

        #region ToNullable(将指定值转换为对应的可空类型)

        /// <summary>
        /// 将指定值转换为对应的可空类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static T? ToNullable<T>(this T value) where T : struct
        {
            return value.IsNull() ? null : (T?)value;
        }

        #endregion
    }
}
