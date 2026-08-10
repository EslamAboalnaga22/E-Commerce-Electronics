using ECommerceElctronics.DataServices.ResultPattern;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ECommerceElctronics.DataServices.Services.Caching
{
    public class RedisServices(IDistributedCache distributedCache) : IRedisServices
    {
        private readonly IDistributedCache _distributedCache = distributedCache;
        public Result<T>? GetData<T>(string key)
        {
            var data = _distributedCache.GetString(key);

            if (data == null) 
                return Result<T>.Failure(new Error("Data not found", "The requested data could not be found in the cache."));

            var cachedData = JsonSerializer.Deserialize<T>(data);

            return Result<T>.Success(cachedData);
        }

        public Result<bool> SetData<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            if(value == null)
                return Result<bool>.Failure(new Error("Invalid data", "The provided data is null and cannot be cached."));

            var data = JsonSerializer.Serialize(value);

            _distributedCache.SetString(key, data, options);

            return Result<bool>.Success(true);
        }

        public Result<bool> RemoveData(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _distributedCache.Remove(key);
                return Result<bool>.Success(true);
            }
            else
                return Result<bool>.Failure(new($"Key is null or empty", "Remove Data From Cache"));
        }
    }
}
