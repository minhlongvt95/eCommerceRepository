using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CouponController : ControllerBase
    {
        public readonly ICouponRepository _couponRepository;
        public CouponController(IServiceProvider provider)
        {
            _couponRepository = provider.GetRequiredService<ICouponRepository>();
        }

        [HttpGet("{productName}", Name = "GetCoupon")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCouponAsync(string productName)
        {
            var response = await _couponRepository.GetCouponAsync(productName);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] Coupon coupon)
        {
            await _couponRepository.CreateCoupon(coupon);
            return CreatedAtRoute("GetCoupon", new { productName = coupon.ProductName },coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] Coupon coupon)
        {
            await _couponRepository.UpdateCoupon(coupon);
            return Ok();
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string productName)
        {
            await _couponRepository.DeleteCouponAsync(productName);
            return Ok();
        }

    }
}
