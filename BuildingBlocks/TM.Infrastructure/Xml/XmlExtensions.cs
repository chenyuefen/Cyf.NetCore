using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TM.Infrastructure.Xml
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public static class XmlExtensions
    {
        #region ----反序列化----

        /// <summary>
        /// 反序列化    xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader reader = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(reader) as T;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string xml) where T : class
        {
            return Deserialize<T>(xml);
        }

        /// <summary>
        /// 反序列化    流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream stream) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化    链接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileUrl"></param>
        /// <param name="whitespaceHandling"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string fileUrl, WhitespaceHandling whitespaceHandling = WhitespaceHandling.All) where T : class
        {
            try
            {
                using (XmlTextReader reader = new XmlTextReader(fileUrl))
                {
                    reader.WhitespaceHandling = whitespaceHandling;
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(reader) as T;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region ----序列化----

        /// <summary>
        /// 序列化  泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// Type 抽象类
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    Type type = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(writer, obj);
                    writer.Close();
                    return writer.ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 序列化 基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(object obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, obj);  //序列化对象
                    StreamReader reader = new StreamReader(stream);
                    string result = reader.ReadToEnd();
                    reader.Dispose();
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
