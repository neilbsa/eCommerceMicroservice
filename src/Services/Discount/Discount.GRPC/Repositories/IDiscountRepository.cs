using Discount.GRPC.Entities;
using System.Threading.Tasks;

namespace Discount.GRPC.Repositories
{
   public interface IDiscountRepository
    {
        Task<Coupon> GetDiscountAsync(string productname);

        Task<bool> CreateDiscountAsync(Coupon coupon);
        Task<bool> UpdateDiscountAsync(Coupon coupon);
        Task<bool> DeleteDiscountAsync(string productname);



    }
}
