namespace PostApi.Infrastructure.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key, Func<T> factory) where T : class;
        T? Get<T>(string key)where T : class;
        void Set<T>(string key, T value) where T : class;
        void Remove(string key);
        void RemoveByPrefix(string prefixKey);
    }
}