using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace PostApi.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private static ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public T Get<T>(string key, Func<T> factory) where T : class
        {
            T? cachedValue = Get<T>(key);
            if (cachedValue is not null)
            {
                return cachedValue;
            }
            cachedValue = factory();
            Set(key, cachedValue);
            return cachedValue;
        }
        public T? Get<T>(string key) where T : class
        {
            string? cacheValue = _distributedCache.GetString(key);
            if (cacheValue is null)
            {
                return null;
            }
            T? value = JsonConvert.DeserializeObject<T>(cacheValue);
            return value;
        }
        public void Set<T>(string key, T value) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            _distributedCache.SetString(key, cacheValue);
            CacheKeys.TryAdd(key, false);
        }
        public void Remove(string key)
        {
            _distributedCache.Remove(key);
            CacheKeys.TryRemove(key, out bool _);
        }
        public void RemoveByPrefix(string prefixKey)
        {
            foreach (var key in CacheKeys.Keys)
            {
                if (key.StartsWith(prefixKey))
                {
                    _distributedCache.Remove(key);
                    CacheKeys.TryRemove(key, out bool _);
                }
            }
        }
        // public Task RemoveByPrefix(string prefixKey)
        // {
        //     IEnumerable<Task> tasks = CacheKeys
        //         .Keys
        //         .Where(key => key.StartsWith(prefixKey))
        //         .Select(key => RemoveAsync(key))
        //         .ToList();
        //     return Task.WhenAll(tasks);
        // }
    }
}