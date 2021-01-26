﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TM.Core.Payment.Alipay
{
    /// <summary>
    /// Alipay通知客户端。
    /// </summary>
    public interface IAlipayNotifyClient
    {
        Task<T> ExecuteAsync<T>(HttpRequest request) where T : AlipayNotifyResponse;
    }
}
