using ECommerceElctronics.DataServices.ResultPattern;

namespace ECommerceElctronics.DataServices.Services.Caching
{
    public interface IRedisServices
    {
        Result<T>? GetData<T>(string key);
        Result<bool> SetData<T>(string key, T value);

        Result<bool> RemoveData(string key);
    }
}
