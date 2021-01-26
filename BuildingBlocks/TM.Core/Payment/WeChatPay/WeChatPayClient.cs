using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TM.Core.Payment.Security;
using TM.Core.Payment.WeChatPay.Parser;
using TM.Core.Payment.WeChatPay.Request;
using TM.Core.Payment.WeChatPay.Utility;

namespace TM.Core.Payment.WeChatPay
{
    public class WeChatPayClient : IWeChatPayClient
    {
        private const string appid = "appid";
        private const string mch_id = "mch_id";
        private const string mch_appid = "mch_appid";
        private const string notify_url = "notify_url";
        private const string mchid = "mchid";
        private const string wxappid = "wxappid";
        private const string sign_type = "sign_type";
        private const string nonce_str = "nonce_str";
        private const string sign = "sign";
        private const string enc_bank_no = "enc_bank_no";
        private const string enc_true_name = "enc_true_name";
        private const string partnerid = "partnerid";
        private const string noncestr = "noncestr";
        private const string timestamp = "timestamp";
        private const string appId = "appId";
        private const string timeStamp = "timeStamp";
        private const string nonceStr = "nonceStr";
        private const string signType = "signType";
        private const string paySign = "paySign";

        private readonly AsymmetricKeyParameter PublicKey;

        public WeChatPayOptions Options { get; set; }

        public virtual ILogger<WeChatPayClient> logger { get; set; }

        protected internal HttpClientEx Client { get; set; }

        protected internal HttpClientEx CertificateClient { get; set; }

        #region WeChatPayClient Constructors

        public WeChatPayClient(
            IOptions<WeChatPayOptions> optionsAccessor,
            ILogger<WeChatPayClient> logger)
        {
            this.logger = logger;
            try
            {
                logger?.LogDebug($"{DateTime.Now} 微信支付初始化日志(1)");

                Options = optionsAccessor.Value;

                Client = new HttpClientEx();

                if (string.IsNullOrEmpty(Options.AppId))
                {
                    throw new ArgumentNullException(nameof(Options.AppId));
                }

                if (string.IsNullOrEmpty(Options.MchId))
                {
                    throw new ArgumentNullException(nameof(Options.MchId));
                }

                if (string.IsNullOrEmpty(Options.Key))
                {
                    throw new ArgumentNullException(nameof(Options.Key));
                }

                if (!string.IsNullOrEmpty(Options.Certificate))
                {
                    logger.LogInformation($"{DateTime.Now} 微信支付初始化日志(3) {Environment.CurrentDirectory}/{Options.Certificate}");

                    var certificate = $"{Environment.CurrentDirectory}/{Options.Certificate}";

                    logger.LogInformation($"{DateTime.Now} 微信支付初始化日志(4) {certificate}");

                    var clientHandler = new HttpClientHandler();

                    logger.LogInformation($"{DateTime.Now} 微信支付初始化日志(5) {Options.MchId}");

                    clientHandler.ClientCertificates.Add(
                        File.Exists(certificate) ? new X509Certificate2(certificate, Options.MchId, X509KeyStorageFlags.MachineKeySet) :
                        new X509Certificate2(Convert.FromBase64String(certificate), Options.MchId, X509KeyStorageFlags.MachineKeySet));

                    logger.LogInformation($"{DateTime.Now} 微信支付初始化日志(6)");

                    CertificateClient = new HttpClientEx(clientHandler);
                }

                logger.LogInformation($"{DateTime.Now} 微信支付初始化日志(7)");
                if (!string.IsNullOrEmpty(Options.RsaPublicKey))
                {
                    PublicKey = RSAUtilities.GetPublicKeyParameterFormAsn1PublicKey(Options.RsaPublicKey);
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"{DateTime.Now} 微信支付初始化日志报错{ex.Message}", ex.Message);
            }
        }

        public WeChatPayClient(IOptions<WeChatPayOptions> optionsAccessor)
            : this(optionsAccessor, null)
        { }

        #endregion

        #region IWeChatPayClient Members

        public void SetTimeout(int timeout)
        {
            try
            {
                Client.Timeout = new TimeSpan(0, 0, 0, timeout);
                if (CertificateClient != null)
                {
                    CertificateClient.Timeout = new TimeSpan(0, 0, 0, timeout);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} SetTimeout(1)", ex.Message);
            }
        }

        #endregion

        #region IWeChatPayClient Members

        public async Task<T> ExecuteAsync<T>(IWeChatPayRequest<T> request) where T : WeChatPayResponse
        {
            try
            {
                // 字典排序
                var sortedTxtParams = new WeChatPayDictionary(request.GetParameters())
                {
                    { mch_id, Options.MchId },
                    { nonce_str, Guid.NewGuid().ToString("N") },
                    { notify_url, Options.NotifyUrl }
                };

                if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                {
                    sortedTxtParams.Add(appid, Options.AppId);
                }

                sortedTxtParams.Add(sign, WeChatPaySignature.SignWithKey(sortedTxtParams, Options.Key));
                var content = HttpClientEx.BuildContent(sortedTxtParams);

                var body = await Client.DoPostAsync(request.GetRequestUrl(), content);

                var parser = new WeChatPayXmlParser<T>();
                var rsp = parser.Parse(body);
                CheckResponseSign(rsp);
                return rsp;
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} ExecuteAsync(1)", ex.Message);
                return null;
            }
        }

        #endregion

        #region IWeChatPayClient Members

        public async Task<T> ExecuteAsync<T>(IWeChatPayCertificateRequest<T> request) where T : WeChatPayResponse
        {
            try
            {
                var signType = true; // ture:MD5，false:HMAC-SHA256
                var excludeSignType = true;

                if (CertificateClient == null)
                {
                    throw new ArgumentNullException(nameof(Options.Certificate));
                }

                // 字典排序
                var sortedTxtParams = new WeChatPayDictionary(request.GetParameters());
                if (request is WeChatPayTransfersRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(mch_appid)))
                    {
                        sortedTxtParams.Add(mch_appid, Options.AppId);
                    }

                    sortedTxtParams.Add(mchid, Options.MchId);
                }
                else if (request is WeChatPayGetPublicKeyRequest)
                {
                    sortedTxtParams.Add(mch_id, Options.MchId);
                    sortedTxtParams.Add(sign_type, "MD5");
                    excludeSignType = false;
                }
                else if (request is WeChatPayPayBankRequest)
                {
                    if (PublicKey == null)
                    {
                        throw new ArgumentNullException(nameof(Options.RsaPublicKey));
                    }

                    var no = RSA_ECB_OAEPWithSHA1AndMGF1Padding.Encrypt(sortedTxtParams.GetValue(enc_bank_no), PublicKey);
                    sortedTxtParams.SetValue(enc_bank_no, no);

                    var name = RSA_ECB_OAEPWithSHA1AndMGF1Padding.Encrypt(sortedTxtParams.GetValue(enc_true_name), PublicKey);
                    sortedTxtParams.SetValue(enc_true_name, name);

                    sortedTxtParams.Add(mch_id, Options.MchId);
                    sortedTxtParams.Add(sign_type, "MD5");
                }
                else if (request is WeChatPayQueryBankRequest)
                {
                    sortedTxtParams.Add(mch_id, Options.MchId);
                    sortedTxtParams.Add(sign_type, "MD5");
                }
                else if (request is WeChatPayGetTransferInfoRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                    {
                        sortedTxtParams.Add(appid, Options.AppId);
                    }

                    sortedTxtParams.Add(mch_id, Options.MchId);
                    sortedTxtParams.Add(sign_type, "MD5");
                }
                else if (request is WeChatPayDownloadFundFlowRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                    {
                        sortedTxtParams.Add(appid, Options.AppId);
                    }

                    sortedTxtParams.Add(mch_id, Options.MchId);
                    signType = false; // HMAC-SHA256
                }
                else if (request is WeChatPayRefundRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                    {
                        sortedTxtParams.Add(appid, Options.AppId);
                    }

                    sortedTxtParams.Add(notify_url, Options.RefundNotifyUrl);
                    sortedTxtParams.Add(mch_id, Options.MchId);
                }
                else if (request is WeChatPaySendRedPackRequest || request is WeChatPaySendGroupRedPackRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(wxappid)))
                    {
                        sortedTxtParams.Add(wxappid, Options.AppId);
                    }

                    sortedTxtParams.Add(mch_id, Options.MchId);
                }
                else // 其他接口
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                    {
                        sortedTxtParams.Add(appid, Options.AppId);
                    }

                    sortedTxtParams.Add(mch_id, Options.MchId);
                }

                sortedTxtParams.Add(nonce_str, Guid.NewGuid().ToString("N"));
                sortedTxtParams.Add(sign, WeChatPaySignature.SignWithKey(sortedTxtParams, Options.Key, signType, excludeSignType));

                var content = HttpClientEx.BuildContent(sortedTxtParams);

                logger.LogInformation($"{DateTime.Now} content:{content}");
                logger.LogInformation($"{DateTime.Now} GetRequestUrl:{request.GetRequestUrl()}");

                var body = await CertificateClient.DoPostAsync(request.GetRequestUrl(), content);
                logger.LogInformation($"{DateTime.Now} body:{body}");

                var parser = new WeChatPayXmlParser<T>();
                var rsp = parser.Parse(body);
                CheckResponseSign(rsp, signType, excludeSignType);
                return rsp;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"{DateTime.Now} ExecuteAsync6666:{ex.Message}", ex.Message);
                return null;
            }
        }

        #endregion

        #region IWeChatPayClient Members

        public Task<WeChatPayDictionary> ExecuteAsync(IWeChatPayCalcRequest request)
        {
            try
            {
                var sortedTxtParams = new WeChatPayDictionary(request.GetParameters());
                if (request is WeChatPayAppCallPaymentRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appid)))
                    {
                        sortedTxtParams.Add(appid, Options.AppId);
                    }

                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(partnerid)))
                    {
                        sortedTxtParams.Add(partnerid, Options.MchId);
                    }
                    sortedTxtParams.Add(noncestr, Guid.NewGuid().ToString("N"));
                    sortedTxtParams.Add(timestamp, WeChatPayUtility.GetTimeStamp());
                    sortedTxtParams.Add(sign, WeChatPaySignature.SignWithKey(sortedTxtParams, Options.Key));
                }
                else if (request is WeChatPayLiteAppCallPaymentRequest || request is WeChatPayH5CallPaymentRequest)
                {
                    if (string.IsNullOrEmpty(sortedTxtParams.GetValue(appId)))
                    {
                        sortedTxtParams.Add(appId, Options.AppId);
                    }

                    sortedTxtParams.Add(timeStamp, WeChatPayUtility.GetTimeStamp());
                    sortedTxtParams.Add(nonceStr, Guid.NewGuid().ToString("N"));
                    sortedTxtParams.Add(signType, "MD5");
                    sortedTxtParams.Add(paySign, WeChatPaySignature.SignWithKey(sortedTxtParams, Options.Key));
                }
                return Task.FromResult(sortedTxtParams);
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} ExecuteAsync", ex.Message);
                return null;
            }
        }

        #endregion

        #region Common Method

        private void CheckResponseSign(WeChatPayResponse response, bool useMD5 = true, bool excludeSignType = true)
        {
            try
            {
                if (string.IsNullOrEmpty(response.Body))
                {
                    throw new Exception("sign check fail: Body is Empty!");
                }

                if (response.Parameters.TryGetValue("sign", out var sign))
                {
                    if (response.Parameters["return_code"] == "SUCCESS" && !string.IsNullOrEmpty(sign))
                    {
                        var cal_sign = WeChatPaySignature.SignWithKey(response.Parameters, Options.Key, useMD5, excludeSignType);
                        if (cal_sign != sign)
                        {
                            throw new Exception("sign check fail: check Sign and Data Fail!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} CheckResponseSign", ex.Message);
            }
        }

        #endregion
    }
}
