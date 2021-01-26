using Newtonsoft.Json;
using StackExchange.Redis;

namespace TM.Infrastructure.Json
{
    /// <summary>
    /// Json辅助扩展操作
    /// </summary>
    public static class JsonExtensions
    {
        #region ToObject(将Json字符串转换为对象)

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JsonUtil.ToObject<T>(json);
        }

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json, System.Type targetType)
        {
            return JsonUtil.ToObject<T>(json);
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object ToObject(this string jsonStr, System.Type type)
        {
            return JsonConvert.DeserializeObject(jsonStr, type);
        }

        /// <summary>
        /// redis对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static T ToObectRedis<T>(this RedisValue redisValue)
        {
            return JsonUtil.ToObject<T>(redisValue);
        }

        #endregion

        #region ToJson(将对象转换为Json字符串)

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转换成单引号</param>
        /// <param name="camelCase">是否驼峰式命名</param>
        /// <param name="indented">是否缩进</param>
        /// <returns></returns>
        public static string ToJson(this object target, bool isConvertToSingleQuotes = false, bool camelCase = false, bool indented = false)
        {
            return JsonUtil.ToJson(target, isConvertToSingleQuotes, camelCase, indented);
        }
        #endregion




    }
}
