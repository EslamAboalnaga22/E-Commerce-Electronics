using ECommerceElctronics.DataServices.ResultPattern;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceElctronics.DataServices.Services.Caching
{
    public class CacheSerivces(IMemoryCache memoryCache) : ICacheSerivces
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        public Result<T> GetData<T>(string key)
        {
            var item = _memoryCache.Get<T>(key);

            if (item == null) 
                return Result<T>.Failure(new($"No data found for key: {key}" , "Get Data From Cache"));

            return Result<T>.Success(item);
        }

        public Result<bool> SetData<T>(string key, T value, DateTimeOffset expireTime)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (value == null)
                    return Result<bool>.Failure(new($"Value is null", "Set Data To Cache"));
                else
                {
                    _memoryCache.Set(key, value, expireTime);

                    return Result<bool>.Success(true);
                }
            }

            else
                return Result<bool>.Failure(new($"Key is null or empty", "Set Data To Cache"));
        }

        public Result<bool> RemoveData(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Remove(key);
                return Result<bool>.Success(true);
            }
            else
                return Result<bool>.Failure(new($"Key is null or empty", "Remove Data From Cache"));
        }
    }
}


