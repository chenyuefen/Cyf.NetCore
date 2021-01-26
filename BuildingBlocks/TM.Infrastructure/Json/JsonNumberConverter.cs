using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace TM.Infrastructure.Json
{
    /// <summary>
    /// 大数字序列化器
    /// </summary>
    public class JsonNumberConverter : JsonConverter
    {
        private readonly Type[] _types;
        /// <summary>
        /// 大数字序列化器
        /// </summary>
        /// <param name="types"></param>
        public JsonNumberConverter(params Type[] types)
        {
            this._types = types;
        }

        /// <summary>
        /// 写入json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type == JTokenType.Integer)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                t.WriteTo(writer);
            }
        }

        /// <summary>
        /// 读取json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false.The type will skip the converter.");
        }

        /// <summary>
        /// 是否可读
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// 判断是否long和decimal
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long) || objectType == typeof(decimal);
        }
    }

}
