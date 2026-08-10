using ECommerceElctronics.DataServices.ResultPattern;

namespace ECommerceElctronics.DataServices.Services.Caching
{
    public interface ICacheSerivces
    {
        Result<T> GetData<T>(string key);
        Result<bool> SetData<T>(string key, T value, DateTimeOffset expireTime);
        Result<bool> RemoveData(string key);
    }
}
