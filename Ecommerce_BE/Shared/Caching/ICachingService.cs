namespace Ecommerce_BE.Shared.Caching
{
  public interface ICachingService<T> 
  {
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T item, TimeSpan timeExpired);
    Task RemoveAsync(string key);
  }
}
