
using Discount.GRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GRPC
{
    public class DiscountGRPC
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGRPC(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountProtoService = discountService;
        }


        public async Task<CouponModel> GetDiscount(string ProductName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = ProductName };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
