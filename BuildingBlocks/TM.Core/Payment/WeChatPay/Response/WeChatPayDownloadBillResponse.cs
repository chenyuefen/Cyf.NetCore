﻿using System.Xml.Serialization;

namespace TM.Core.Payment.WeChatPay.Response
{
    [XmlRoot("xml")]
    public class WeChatPayDownloadBillResponse : WeChatPayResponse
    {
        /// <summary>
        /// 返回状态码
        /// 此字段是通信标识，非交易标识，
        /// 交易是否成功需要查看result_code来判断
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息，如非空，为错误原因
        /// 签名失败
        /// 参数格式校验错误
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }
    }
}
