using Microsoft.Extensions.Caching.Memory;

namespace CrossDSP.Infrastructure.Extensions
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
            if (memoryCache.TryGetValue(key, out object? item) && item != null)
            {
                return (TItem?) item;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(expiryTimeInSeconds));

            var result = await func();
            memoryCache.Set(key, result, cacheEntryOptions);

            return result;
        }

        public static TItem? TryGetItem<TItem>(
            this IMemoryCache memoryCache,
            string key
        )
        {
            if (memoryCache.TryGetValue(key, out object? item))
            {
                return (TItem?)item;
            }

            return default;
        }
    }
}