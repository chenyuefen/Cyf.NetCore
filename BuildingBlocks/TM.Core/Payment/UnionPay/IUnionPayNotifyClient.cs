using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TM.Core.Payment.UnionPay
{
    public interface IUnionPayNotifyClient
    {
        Task<T> ExecuteAsync<T>(HttpRequest request) where T : UnionPayNotifyResponse;
    }
}
