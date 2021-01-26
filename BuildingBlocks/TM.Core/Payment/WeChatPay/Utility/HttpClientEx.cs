using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TM.Core.Payment.WeChatPay.Utility
{
    public sealed class HttpClientEx : HttpClient
    {
        public HttpClientEx() : base()
        {
            try
            {
                Timeout = new TimeSpan(0, 0, 0, 10);
                DefaultRequestHeaders.Connection.Add("keep-alive");
            }
            catch (Exception ex)
            {

            }
        }

        public HttpClientEx(HttpClientHandler handler) : base(handler)
        {
            try
            {
                Timeout = new TimeSpan(0, 0, 0, 10);
                DefaultRequestHeaders.Connection.Add("keep-alive");
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求参数</param>
        /// <returns>HTTP响应</returns>
        public async Task<string> DoPostAsync(string url, string content)
        {
            try
            {
                using (var requestContent = new StringContent(content, Encoding.UTF8, "application/xml"))
                using (var response = await PostAsync(url, requestContent))
                using (var responseContent = response.Content)
                {
                    return await responseContent.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildContent(IDictionary<string, string> parameters)
        {
            try
            {
                var content = new StringBuilder("<xml>");
                foreach (var iter in parameters)
                {
                    if (!string.IsNullOrEmpty(iter.Value))
                    {
                        content.Append("<" + iter.Key + ">" + "<![CDATA[" + iter.Value + "]]></" + iter.Key + ">");
                    }
                }
                return content.Append("</xml>").ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
