using Discount.gRPC.Entities;

namespace Discount.gRPC.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponAsync(string productName);

        Task<bool> CreateCoupon(Coupon coupon);

        Task<bool> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCouponAsync(string productName);
    }
}
