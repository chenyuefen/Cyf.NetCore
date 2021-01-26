using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TM.Core.Payment.WeChatPay
{
    public interface IWeChatPayNotifyClient
    {
        Task<T> ExecuteAsync<T>(HttpRequest request) where T : WeChatPayNotifyResponse;
    }
}
