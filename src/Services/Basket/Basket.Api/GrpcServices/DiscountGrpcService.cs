using Basket.Api.Repositories;
using Discount.gRPC.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountGrpcService(IServiceProvider provider)
        {
            _discountService = provider.GetRequiredService<DiscountProtoService.DiscountProtoServiceClient>();
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var getDiscountReq = new GetDiscountRequest { ProductName = productName };

            return await _discountService.GetDiscountAsync(getDiscountReq);
        }
    }
}
