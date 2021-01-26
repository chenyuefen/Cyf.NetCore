using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TM.Infrastructure.FuLu.Module;

namespace TM.Infrastructure.FuLu
{
    public class FuLuHelper
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public static async Task<ResponseParam> GetGoodsList()
        {
            return await SendFuLuRequest(FuLuConfig.GoodsListGet, null);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns></returns>
        public static async Task<ResponseParam> GetGoodsInfo(string product_id)
        {
            var data = new Dictionary<string, string>();
            data.Add("product_id", product_id.ToString());

            return await SendFuLuRequest(FuLuConfig.GoodsInfoGet, data);
        }

        /// <summary>
        /// 直充下单接口
        /// </summary>
        /// <param name="product_id">商品编号</param>
        /// <param name="customer_order_no">外部订单号</param>
        /// <param name="charge_account">充值账号</param>
        /// <param name="buy_num">购买数量</param>
        /// <returns></returns>
        public static async Task<ResponseParam> OrderDirectAdd(long product_id, string customer_order_no, string charge_account,
            int buy_num)
        {
            var data = new Dictionary<string, string>();
            data.Add("product_id", product_id.ToString());
            data.Add("customer_order_no", customer_order_no);
            data.Add("charge_account", charge_account);
            data.Add("buy_num", buy_num.ToString());

            return await SendFuLuRequest(FuLuConfig.OrderDirectAdd, data);
        }

        /// <summary>
        /// 卡密下单接口
        /// </summary>
        /// <param name="product_id">商品编号</param>
        /// <param name="customer_order_no">外部订单号</param>
        /// <param name="buy_num">购买数量</param>
        /// <returns></returns>
        public static async Task<ResponseParam> OrderCardAdd(long product_id, string customer_order_no, int buy_num)
        {
            var data = new Dictionary<string, string>();
            data.Add("product_id", product_id.ToString());
            data.Add("customer_order_no", customer_order_no);
            data.Add("buy_num", buy_num.ToString());

            return await SendFuLuRequest(FuLuConfig.OrderCardAdd, data);
        }

        /// <summary>
        /// 话费下单接口
        /// </summary>
        /// <param name="charge_phone">充值手机号</param>
        /// <param name="customer_order_no">外部订单号</param>
        /// <param name="charge_value">充值数额</param>
        /// <returns></returns>
        public static async Task<ResponseParam> OrderMobileAdd(string charge_phone, string customer_order_no, string charge_value)
        {
            var data = new Dictionary<string, string>();
            data.Add("charge_phone", charge_phone);
            data.Add("customer_order_no", customer_order_no);
            data.Add("charge_value", charge_value);

            return await SendFuLuRequest(FuLuConfig.OrderMobileAdd, data);
        }

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="customer_order_no">外部订单号</param>
        /// <returns></returns>
        public static async Task<ResponseParam> OrderInfoGet(string customer_order_no)
        {
            var data = new Dictionary<string, string>();
            data.Add("customer_order_no", customer_order_no);

            return await SendFuLuRequest(FuLuConfig.OrderInfoGet, data);
        }

        /// <summary>
        /// 对账单申请接口
        /// </summary>
        /// <param name="customer_order_no">外部订单号</param>
        /// <returns></returns>
        public static async Task<ResponseParam> OrderRecordGet(string order_record_get_no, string topic, string reconciliation_type,
            string begin_create_time, string end_create_time)
        {
            var data = new Dictionary<string, string>();
            data.Add("order_record_get_no", order_record_get_no);
            data.Add("topic", topic);
            data.Add("reconciliation_type", reconciliation_type);
            data.Add("begin_create_time", begin_create_time);
            data.Add("end_create_time", end_create_time);

            return await SendFuLuRequest(FuLuConfig.OrderRecordGet, data);
        }

        /// <summary>
        /// 手机号归属地接口
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static async Task<ResponseParam> MobileInfoGet(string phone)
        {
            var data = new Dictionary<string, string>();
            data.Add("phone", phone);

            return await SendFuLuRequest(FuLuConfig.MobileInfoGet, data);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<ResponseParam> SendFuLuRequest(string method, object data)
        {
            return await SendFuLuRequest(method, data, FuLuConfig.Url);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<ResponseParam> SendFuLuRequest(string method, object data, string url)
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("method", method);
            dictionary.Add("biz_content", data == null ? "{}" : JsonConvert.SerializeObject(data));

            SetSign(dictionary);

            var client = new RestClient(url);
            var request = new RestRequest(method: Method.POST);
            request.AddJsonBody(dictionary);

            var response = await client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<ResponseParam>(response.Content);

            CheckSign(content.result, content.sign);

            return content;
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static void CheckSign(string data, string sign)
        {
            if (string.IsNullOrWhiteSpace(data))
                return;

            var chars = data.ToCharArray();
            Array.Sort(chars);

            var value = new string(chars) + FuLuConfig.AppSecret;
            var p = Md5(value).ToLower();
            if (p != sign)
                throw new Exception($"数据被篡改，data={data}");
        }

        /// <summary>
        /// 设置Sign
        /// </summary>
        /// <param name="dictionary"></param>
        private static void SetSign(Dictionary<string, string> dictionary)
        {
            dictionary.Add("app_key", FuLuConfig.AppKey);
            dictionary.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dictionary.Add("version", FuLuConfig.Version);
            dictionary.Add("format", "json");
            dictionary.Add("charset", "utf-8");
            dictionary.Add("sign_type", "md5");
            dictionary.Add("app_auth_token", "");

            string jsonData = JsonConvert.SerializeObject(dictionary);
            var chars = jsonData.ToCharArray();
            Array.Sort(chars);

            string data = new string(chars) + FuLuConfig.AppSecret;
            string sign = Md5(data).ToLower();

            dictionary.Add("sign", sign);
        }

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Md5(string str)
        {
            Encoding encode = Encoding.UTF8;
            var cl = str;
            var md5 = MD5.Create();
            var s = md5.ComputeHash(encode.GetBytes(cl));
            return s.Aggregate("", (current, t) => current + t.ToString("x2"));
        }


        /// <summary>
        /// 卡密解析
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptAes(string source)
        {
            try
            {
                var key = FuLuConfig.AppSecret;
                //var key = "0a091b3aa4324435aab703142518a8f7";

                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Key = GetAesKey(key);
                    aesProvider.Mode = CipherMode.ECB;
                    aesProvider.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                    {
                        byte[] inputBuffers = Convert.FromBase64String(source);
                        byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                        aesProvider.Clear();
                        return Encoding.UTF8.GetString(results);
                    }
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Aes32位密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <returns>Aes32位密钥</returns>
        private static byte[] GetAesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Encoding.UTF8.GetBytes(key);
        }
    }
}
