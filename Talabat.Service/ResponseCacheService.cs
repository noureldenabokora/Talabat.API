using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class ResponseCacheService : IResponseCacheSrevice
    {
        private readonly IDatabase _database;

        // will store cache data in redis so  ask clr inject ibject form redis 
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;
            // to save option with camel case to understand it as json
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // serliaxe resonse to json
            var serilizedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey, serilizedResponse, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            if (cachedResponse.IsNullOrEmpty) return null;
            return cachedResponse;
        }
    }
}
