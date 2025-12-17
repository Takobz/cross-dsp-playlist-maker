using Microsoft.Extensions.Caching.Memory;

namespace CrossDSP.WEBAPI.Extensions
{
    public static class IMemoryCacheExtensions
    {
        public async static Task<TItem?> GetOrSetItemAsync<TItem>(
            this IMemoryCache memoryCache,
            string key,
            Func<Task<TItem>> func,
            int expiryTimeInSeconds
        )
        {
            if (memoryCache.TryGetValue(key, out object? item))
            {
                return (TItem?) item;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(expiryTimeInSeconds));

            var result = await func();
            memoryCache.Set(key, result, cacheEntryOptions);

            return result;
        }
    }
}