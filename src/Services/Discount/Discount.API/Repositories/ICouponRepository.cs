using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponAsync(string productName);

        Task<bool> CreateCoupon(Coupon coupon);

        Task<bool> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCouponAsync(string productName);
    }
}
