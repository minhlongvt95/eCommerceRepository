using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class BasketController : ControllerBase
    {
        public IBasketRepository _basketRepository { get; }
        public BasketController(IServiceProvider provider)
        {
            _basketRepository = provider.GetRequiredService<IBasketRepository>();
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string username)
        {
            var response = await _basketRepository.GetBasket(username);
            return Ok(response ?? new ShoppingCart(username));
        }


        [HttpPut(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            var response = await _basketRepository.UpdateBasket(basket);
            return Ok(response);
        }


        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }
    }
}
