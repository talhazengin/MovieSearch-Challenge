using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MovieSearch.Core;

namespace MovieSearch
{
    public static class DistributedCaching
    {
        public static async Task SetAsync<T>(this IDistributedCache distributedCache,
            string key, T value, DistributedCacheEntryOptions options = null, CancellationToken token = default)
        {
            if (options == null)
                options = new DistributedCacheEntryOptions();

            await distributedCache.SetAsync(key, value.ToByteArray(), options, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache,
            string key, CancellationToken token = default)
            where T : class
        {
            byte[] result = await distributedCache.GetAsync(key, token);
            return result.FromByteArray<T>();
        }
    }
}
