using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public readonly IDistributedCache _distributedCache;
        public BasketRepository(IServiceProvider provider)
        {
            _distributedCache = provider.GetRequiredService<IDistributedCache>();
        }
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _distributedCache.GetStringAsync(username);
            if (basket == null)
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _distributedCache.SetStringAsync(basket.username, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.username);
        }
        
        public async Task DeleteBasket(string username)
        {
            await _distributedCache.RemoveAsync(username);
        }
    }
}
