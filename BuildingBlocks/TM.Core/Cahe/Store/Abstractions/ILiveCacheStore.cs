using System;
using System.Threading.Tasks;

namespace TM.Core.Cahe.Store.Abstractions
{
    public interface ILiveCacheStore
    {
        Task<T> InitCacheOutDto<T>(int keyType, string key);

        Task<T> GetCacheOutDtoAsync<T>(int keyType, string key);

        Task AddCacheAsync<T>(int keyType, string key, T value);

        Task AddCacheAsync<T>(int keyType, string key, T value, TimeSpan timeSpan);

        Task<bool> IsExistAsync(int keyType, string key);

        Task DelCacheAsync(int keyType, string key);
    }
}
