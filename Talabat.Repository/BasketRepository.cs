using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            // get basket first 
            var basket = await _database.StringGetAsync(basketId);
           // check if is null if don't need to convert it from json to customer basket 
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket); 
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {                                                             // pass id and            convert from json                   then time before deleted
            var updatedOrCreatedBasket = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
            if(updatedOrCreatedBasket is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
