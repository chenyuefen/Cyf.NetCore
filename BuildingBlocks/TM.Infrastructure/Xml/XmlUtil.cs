using System.IO;
using System.Text;
using System.Xml;

namespace TM.Infrastructure.Xml
{
    /// <summary>
    /// Json操作辅助类
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 把XmlDocument对象转化成xml字符串--用于req
        /// </summary>
        /// <param name="xmlDocument">需要转化成xml字符串的XmlDocument对象</param>
        /// <returns>xml字符串</returns>
        public static string XmlDocument2XmlString(XmlDocument xmlDocument)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);

            xmlDocument.WriteTo(writer);
            string destXmlString = sw.ToString();
            writer.Close();

            return destXmlString;
        }


        /// <summary>
        /// 把xml字符串转换成XmlDocument对象-用于res/req-test
        /// </summary>
        /// <param name="xmlString">xml字符串</param>
        /// <returns>XmlDocument对象</returns>
        public static XmlDocument XmlString2XmlDocument(string xmlString)
        {
            XmlDocument doc = new XmlDocument();

            doc.PreserveWhitespace = true;//Keep it.

            MemoryStream ms = new MemoryStream(new UTF8Encoding().GetBytes(xmlString));
            doc.Load(ms);

            return doc;
        }

        /// <summary>
        /// 从XmlDocument中取得指定标签的值
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="tagName">标签</param>
        /// <returns>XmlDocument对象</returns>
        public static string GetNodeText(XmlDocument doc, string tagName)
        {
            XmlNodeList elemList = doc.GetElementsByTagName(tagName);
            if (elemList != null && elemList[0] != null)
            {
                return elemList[0].InnerXml;
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// 获取指定node的指定child标签值
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <param name="childName">child标签</param>
        /// <returns>String</returns>
        public static string GetChildNodeText(XmlNode node, string childName)
        {
            XmlNodeList children = node.ChildNodes;
            int len = children.Count;
            for (int i = 0; i < len; i++)
            {
                XmlNode child = children.Item(i);
                if (child.NodeType == XmlNodeType.Element && child.Name == childName)
                {
                    return child.InnerXml;
                }
            }
            return "";
        }


        public static string FormatXmlString(string xmlString)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.Formatting = Formatting.Indented;

            XmlString2XmlDocument(xmlString).WriteTo(writer);
            string destXmlString = sw.ToString();
            writer.Close();

            return destXmlString;
        }

        public static XmlNode GetChildNodeByName(XmlNode parentNode, string nodeName)
        {
            XmlNode childNode = parentNode.FirstChild;
            while (childNode != null)
            {
                if (nodeName.Equals(childNode.Name))
                {
                    return childNode;
                }
                childNode = childNode.NextSibling;
            }
            return null;
        }
    }
}
