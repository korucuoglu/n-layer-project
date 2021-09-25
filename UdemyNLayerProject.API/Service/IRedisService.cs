
using System.Threading.Tasks;

namespace UdemyNLayerProject.API.Service
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key);
        void SetAsync(string key, object data);
        Task<bool> IsKeyAsync(string key);
        Task<bool> RemoveAsync(string key);
    }
}
