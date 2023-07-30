using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repositories;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        public readonly ICouponRepository _couponRepository;
        public readonly ILogger<DiscountService> _logger;
        public readonly IMapper _mapper;

        public DiscountService(IServiceProvider provider)
        {
            _couponRepository = provider.GetRequiredService<ICouponRepository>();
            _logger = provider.GetRequiredService<ILogger<DiscountService>>();
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetCouponAsync(request.ProductName);

            if (coupon == null)
                throw new RpcException(new Status( StatusCode.NotFound, ""));

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _couponRepository.CreateCoupon(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _couponRepository.UpdateCoupon(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteResponseModel> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _couponRepository.DeleteCouponAsync(request.ProductName);

            return new DeleteResponseModel() { Success = result };
        }
    }
}
