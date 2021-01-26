/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：XmlUtil
// 文件功能描述: XmlUtil
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日18:16:38
0// 
//----------------------------------------------------------------*/

using System.IO;
using System.Xml.Serialization;

public class WePayXmlUtil
{
    public static T XmlToObect<T>(string xml)
    {
        using (StringReader sr = new StringReader(xml))
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return (T)xmldes.Deserialize(sr);
        }
    }

}