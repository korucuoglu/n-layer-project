using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace UdemyNLayerProject.API.Service
{
    public class RedisService : CacheHelper, IRedisService
    {
      
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _redis;
        
        public IDatabase db { get; set; }

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];

            _redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = ConnectionMultiplexer.Connect(configString);
            db = _redis.GetDatabase(0);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await db.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                 return  default(T);
            }

            var result = Deserialize<T>(value);
            return result;

        }

        public async void SetAsync(string key, object data)
        {

            var serializeData = Serialize(data);
            await db.StringSetAsync(key, serializeData);
        }

        public async Task<bool> IsKeyAsync(string key)
        {
            return await db.KeyExistsAsync(key);
        }
        public async Task<bool> RemoveAsync(string key)
        {
            return await db.KeyDeleteAsync(key);
        }


    }
}


