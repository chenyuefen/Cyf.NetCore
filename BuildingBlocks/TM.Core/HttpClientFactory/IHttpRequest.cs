using System.Threading.Tasks;

namespace TM.Core.HttpClientFactory
{
    public interface IHttpRequest
    {
        Task<TResponse> SendGetAsync<TResponse>(string url);
        Task<TResponse> SendPostAsync<TRequest, TResponse>(string url, TRequest data, string token = null, string contentType = "application/json");
        Task<bool> GetStatusCodeAsync(string url);
    }
}
