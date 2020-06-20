using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helpers
{
    /// <summary>
    /// 钉钉机器人帮助类
    /// https://ding-doc.dingtalk.com/doc#/serverapi2/qf2nxq
    /// </summary>
    public class DingtalkRobotHelper
    {
        private static string _urlBase;
        private static string _secret;
        private static string _url => _secret == default ? _urlBase : GetUrl(_urlBase, _secret);
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate });

        public static void SetUrl(string urlBase, string secret = default)
        {
            _urlBase = urlBase;
            _secret = secret;
        }

        public static Task<string> SendText(TextBody body) => Send(body);

        public static Task<string> SendMarkdown(MarkdownBody body) => Send(body);

        private static async Task<string> Send(Body body)
        {
            var bodyStr = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            var response = await _httpClient.PostAsync(_url, new StringContent(bodyStr, Encoding.UTF8, "application/json"));
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        private static string GetUrl(string url, string secret)
        {
            var timeStamp = (DateTimeOffset.Now.UtcTicks - 621355968000000000) / 10000;
            var stringToSign = $"{timeStamp}\n{secret}";
            var b64 = GetHmac(stringToSign, secret);
            var b64Str = Convert.ToBase64String(b64);
            var sign = HttpUtility.UrlEncode(b64Str, Encoding.UTF8);
            var newUrl = $"{url}&timestamp={timeStamp}&sign={sign}";
            return newUrl;
        }

        private static byte[] GetHmac(string message, string secret)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return hashmessage;
            }
        }

        public class Body
        {
            public Body(string msgtype)
            {
                this.msgtype = msgtype;
            }

            public string msgtype { get; set; }
            public At at { get; set; }
            public class At
            {
                public string[] atMobiles { get; set; }
                public bool isAtAll { get; set; }
            }
        }

        public class MarkdownBody : Body
        {
            public MarkdownBody(string title, string content) : base("markdown")
            {
                markdown = new Markdown
                {
                    title = title,
                    text = content,
                };
            }
            public Markdown markdown { get; set; }
            public class Markdown
            {
                public string title { get; set; }
                public string text { get; set; }
            }
        }

        public class TextBody : Body
        {
            public TextBody(string content) : base("text")
            {
                text = new Text
                {
                    content = content,
                };
            }

            public Text text { get; set; }

            public class Text
            {
                public string content { get; set; }
            }
        }

    }
}
