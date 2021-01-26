using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TM.Core.Payment.Security;
using TM.Core.Payment.WeChatPay.Notify;
using TM.Core.Payment.WeChatPay.Parser;
using TM.Core.Payment.WeChatPay.Utility;

namespace TM.Core.Payment.WeChatPay
{
    public class WeChatPayNotifyClient : IWeChatPayNotifyClient
    {
        public WeChatPayOptions Options { get; set; }

        public virtual ILogger<WeChatPayNotifyClient> logger { get; set; }

        #region WeChatPayNotifyClient Constructors

        public WeChatPayNotifyClient(
            IOptions<WeChatPayOptions> optionsAccessor,
            ILogger<WeChatPayNotifyClient> logger)
        {
            this.logger = logger;
            try
            {
                logger?.LogDebug($"{DateTime.Now} 初始化微信支付回调");

                Options = optionsAccessor.Value;
                if (string.IsNullOrEmpty(Options.Key))
                {
                    throw new ArgumentNullException(nameof(Options.Key));
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 初始化微信支付回调报错", ex.Message);
            }
        }

        public WeChatPayNotifyClient(IOptions<WeChatPayOptions> optionsAccessor)
            : this(optionsAccessor, null)
        { }

        #endregion

        #region IWeChatPayNotifyClient Members

        public async Task<T> ExecuteAsync<T>(HttpRequest request) where T : WeChatPayNotifyResponse
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} ExecuteAsync微信处理请求头");
                request.EnableBuffering();
                var body = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();

                logger?.LogDebug($"{DateTime.Now} ExecuteAsync Request:{body}");

                var parser = new WeChatPayXmlParser<T>();
                var rsp = parser.Parse(body);

                if (rsp is WeChatPayRefundNotifyResponse)
                {
                    var key = MD5.Compute(Options.Key).ToLower();
                    var data = AES.Decrypt((rsp as WeChatPayRefundNotifyResponse).ReqInfo, key, AESCipherMode.ECB, AESPaddingMode.PKCS7);

                    logger?.LogDebug($"{DateTime.Now} ExecuteAsync Decrypt Content:{data}");// AES-256-ECB
                    rsp = parser.Parse(body, data);
                }
                else
                {
                    CheckNotifySign(rsp);
                }
                logger.LogError($"{DateTime.Now} ExecuteAsync微信处理请求头成功");
                return rsp;
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} ExecuteAsync处理微信请求头报错", ex.Message);
                return null;
            }
        }

        #endregion

        #region Common Method

        private void CheckNotifySign(WeChatPayNotifyResponse response)
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} CheckNotifySign检查微信签名");

                if (response?.Parameters?.Count == 0)
                {
                    logger?.LogError("sign check fail: Body is Empty!");
                    throw new Exception("sign check fail: Body is Empty!");
                }

                if (!response.Parameters.TryGetValue("sign", out var sign))
                {
                    logger?.LogError("sign check fail: sign is Empty!");
                    throw new Exception("sign check fail: sign is Empty!");
                }

                var cal_sign = WeChatPaySignature.SignWithKey(response.Parameters, Options.Key);
                if (cal_sign != sign)
                {
                    logger?.LogError("sign check fail: check Sign and Data Fail!");
                    throw new Exception("sign check fail: check Sign and Data Fail!");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} CheckNotifySign检查微信签名报错", ex.Message);
            }
        }

        #endregion
    }
}