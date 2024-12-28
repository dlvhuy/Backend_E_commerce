using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce_BE.Shared.Caching
{
  public class CachingService<T> : ICachingService<T> where T : class
  {
    private readonly IMemoryCache _cache;
    public CachingService(IMemoryCache cache)
    {
      _cache = cache;
    }
    public async Task<T> GetAsync<T>(string key)
    {
      T? item = _cache.TryGetValue(key, out T? value) ? value : default;
      return item;
    }

    public async Task RemoveAsync(string key)
    {
      _cache.Remove(key);
      await Task.CompletedTask;
    }

    public async Task SetAsync<T>(string key, T item, TimeSpan timeExpired)
    {
      _cache.Set(key, item, new MemoryCacheEntryOptions
      {
        AbsoluteExpirationRelativeToNow = timeExpired
      });
      await Task.CompletedTask;
    }
  }
}
