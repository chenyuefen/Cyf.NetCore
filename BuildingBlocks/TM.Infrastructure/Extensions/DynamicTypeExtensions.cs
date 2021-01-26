﻿using System;

namespace TM.Infrastructure.Extensions
{
    public static class DynamicTypeExtensions
    {
        /// <summary>
        /// Returns true if the type is one of the built in simple types.
        /// </summary>
        public static bool IsSimpleType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = type.GetGenericArguments()[0]; // or  Nullable.GetUnderlyingType(type)

            if (type.IsEnum)
                return true;

            if (type == typeof(Guid))
                return true;

            TypeCode tc = Type.GetTypeCode(type);
            switch (tc)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Char:
                case TypeCode.String:
                case TypeCode.Boolean:
                case TypeCode.DateTime:
                    return true;
                case TypeCode.Object:
                    return (typeof(TimeSpan) == type) || (typeof(DateTimeOffset) == type);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 判断是否为基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBulitinType(this Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        public static bool IsNullable(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


    }
}
