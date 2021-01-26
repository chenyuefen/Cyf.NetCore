using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TM.Core.Payment.Alipay.Parser;
using TM.Core.Payment.Alipay.Utility;
using TM.Core.Payment.Security;

namespace TM.Core.Payment.Alipay
{
    public class AlipayNotifyClient : IAlipayNotifyClient
    {
        private readonly RSAParameters PublicRSAParameters;

        public AlipayOptions Options { get; }

        public virtual ILogger<AlipayNotifyClient> logger { get; set; }

        #region AlipayNotifyClient Constructors

        public AlipayNotifyClient(
            IOptions<AlipayOptions> optionsAccessor,
            ILogger<AlipayNotifyClient> logger)
        {
            this.logger = logger;
            try
            {
                Options = optionsAccessor.Value;
                if (string.IsNullOrEmpty(Options.RsaPublicKey))
                {
                    throw new ArgumentNullException(nameof(Options.RsaPublicKey));
                }
                PublicRSAParameters = RSAUtilities.GetRSAParametersFormPublicKey(Options.RsaPublicKey);
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 支付宝回调初始化日志报错", ex.Message);
            }
        }

        public AlipayNotifyClient(IOptions<AlipayOptions> optionsAccessor)
            : this(optionsAccessor, null)
        { }

        #endregion

        #region IAlipayNotifyClient Members

        public async Task<T> ExecuteAsync<T>(HttpRequest request) where T : AlipayNotifyResponse
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} 支付宝回调ExecuteAsync");

                var parameters = await GetParametersAsync(request);
                var query = HttpClientEx.BuildQuery(parameters);

                logger?.LogDebug($"{DateTime.Now} 支付宝回调ExecuteAsync Request:{query}");

                var parser = new AlipayDictionaryParser<T>();
                var rsp = parser.Parse(parameters);
                CheckNotifySign(parameters, PublicRSAParameters, Options.SignType);
                return rsp;
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 支付宝回调ExecuteAsync报错", ex.Message);
                return null;
            }
        }

        #endregion

        #region Common Method

        private async Task<SortedDictionary<string, string>> GetParametersAsync(HttpRequest request)
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} 支付宝回调GetParametersAsync");

                var parameters = new SortedDictionary<string, string>();
                if (request.Method == "POST")
                {
                    var form = await request.ReadFormAsync();
                    foreach (var iter in form)
                    {
                        parameters.Add(iter.Key, iter.Value);
                    }
                }
                else
                {
                    foreach (var iter in request.Query)
                    {
                        parameters.Add(iter.Key, iter.Value);
                    }
                }
                return parameters;
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 支付宝回调GetParametersAsync报错", ex.Message);
                return null;
            }
        }

        private void CheckNotifySign(IDictionary<string, string> parameters, RSAParameters publicRSAParameters, string signType)
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} 支付宝回调CheckNotifySign");

                if (parameters == null || parameters.Count == 0)
                {
                    throw new Exception("sign check fail: content is Empty!");
                }

                if (!parameters.TryGetValue("sign", out var sign))
                {
                    throw new Exception("sign check fail: sign is Empty!");
                }

                var prestr = GetSignContent(parameters);
                if (!AlipaySignature.RSACheckContent(prestr, sign, publicRSAParameters, signType))
                {
                    throw new Exception("sign check fail: check Sign Data Fail!");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 支付宝回调CheckNotifySign报错", ex.Message);
            }
        }

        private string GetSignContent(IDictionary<string, string> parameters)
        {
            try
            {
                logger?.LogDebug($"{DateTime.Now} 支付宝回调GetSignContent");

                if (parameters == null || parameters.Count == 0)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var sb = new StringBuilder();
                foreach (var iter in parameters)
                {
                    if (!string.IsNullOrEmpty(iter.Value) && iter.Key != "sign" && iter.Key != "sign_type")
                    {
                        sb.Append(iter.Key).Append("=").Append(iter.Value).Append("&");
                    }
                }
                return sb.Remove(sb.Length - 1, 1).ToString();
            }
            catch (Exception ex)
            {
                logger?.LogError($"{DateTime.Now} 支付宝回调GetSignContent报错", ex.Message);
                return null;
            }
        }

        #endregion
    }
}