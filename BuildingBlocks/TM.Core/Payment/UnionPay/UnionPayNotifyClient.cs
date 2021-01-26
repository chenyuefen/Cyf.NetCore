﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TM.Core.Payment.UnionPay.Parser;
using TM.Core.Payment.UnionPay.Utility;

namespace TM.Core.Payment.UnionPay
{
    public class UnionPayNotifyClient : IUnionPayNotifyClient
    {
        private readonly UnionPayCertificate MiddleCertificate;
        private readonly UnionPayCertificate RootCertificate;

        public UnionPayOptions Options { get; }

        public virtual ILogger Logger { get; set; }

        #region UnionPayNotifyClient Constructors

        public UnionPayNotifyClient(
            IOptions<UnionPayOptions> optionsAccessor,
            ILogger<UnionPayNotifyClient> logger)
        {
            Options = optionsAccessor.Value;
            Logger = logger;

            if (string.IsNullOrEmpty(Options.MiddleCert))
            {
                throw new ArgumentNullException(nameof(Options.MiddleCert));
            }

            if (string.IsNullOrEmpty(Options.RootCert))
            {
                throw new ArgumentNullException(nameof(Options.RootCert));
            }

            MiddleCertificate = UnionPaySignature.GetCertificate(Options.MiddleCert);
            RootCertificate = UnionPaySignature.GetCertificate(Options.RootCert);
        }

        public UnionPayNotifyClient(IOptions<UnionPayOptions> optionsAccessor)
            : this(optionsAccessor, null)
        { }

        #endregion

        #region IUnionPayNotifyClient Members

        public async Task<T> ExecuteAsync<T>(HttpRequest request) where T : UnionPayNotifyResponse
        {
            var parameters = await GetParametersAsync(request);

            var query = HttpClientEx.BuildQuery(parameters);
            Logger?.LogTrace(0, "Request:{query}", query);

            var parser = new UnionPayDictionaryParser<T>();
            var rsp = parser.Parse(parameters);
            CheckNotifySign(parameters);
            return rsp;
        }

        #endregion

        #region Common Method

        private async Task<UnionPayDictionary> GetParametersAsync(HttpRequest request)
        {
            var parameters = new UnionPayDictionary();
            var form = await request.ReadFormAsync();
            foreach (var iter in form)
            {
                parameters.Add(iter.Key, iter.Value);
            }
            return parameters;
        }

        private void CheckNotifySign(UnionPayDictionary dic)
        {
            if (dic == null || dic.Count == 0)
            {
                throw new Exception("sign check fail: sign is Empty!");
            }

            var ifValidateCNName = !Options.TestMode;
            if (!UnionPaySignature.Validate(dic, RootCertificate.cert, MiddleCertificate.cert, Options.SecureKey, ifValidateCNName))
            {
                throw new Exception("sign check fail: check Sign and Data Fail!");
            }
        }

        #endregion
    }
}
