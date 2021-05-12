using AutoMapper;
using Discount.GRPC.Data;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {

        private readonly IDiscountRepository _context;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository context, ILogger<DiscountService> logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            try
            {
              await  _context.CreateDiscountAsync(coupon);
                _logger.LogInformation("new disocunt is now added");

            }
            catch (Exception)
            {

                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Error in adding new coupons"));
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.GetDiscountAsync(request.ProductName);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productname { request.ProductName } is not found"));
            }
            _logger.LogInformation($"Discount is not retrieved from document : { coupon.ProductName }, Amount : { coupon.Amount }");
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            try
            {
                await _context.UpdateDiscountAsync(coupon);
                _logger.LogInformation("new disocunt is now added");

            }
            catch (Exception)
            {
                _logger.LogError($"error occured in adding coupon {coupon.ProductName}");
                throw new RpcException(new Status(StatusCode.Internal, $"Error in adding new coupons"));
                
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            DeleteDiscountResponse response = new DeleteDiscountResponse();

            try
            {
                var deleted = await _context.DeleteDiscountAsync(request.ProductName);
                response = new DeleteDiscountResponse() {

                    Success = deleted
                };
            }
            catch (Exception)
            {
                _logger.LogError($"error occured in adding coupon {request.ProductName}");
                throw new RpcException(new Status(StatusCode.Internal, $"Error in deleting coupons"));
            }

            return response;

        }


   

    }
}
