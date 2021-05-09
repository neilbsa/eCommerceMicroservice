
using Discount.API.Data;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {

        private DiscountContext _cont;
        public DiscountRepository(DiscountContext cont)
        {
            _cont = cont;
        }

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            try
            {
                _ = await _cont.Coupons.AddAsync(coupon);
                    await _cont.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
             
            }
            

        }

        public async Task<bool> DeleteDiscountAsync(string productname)
        {
            var forDelete = _cont.Coupons.Where(x => x.ProductName == productname).FirstOrDefault();
            if (forDelete != null)
            {
                _cont.Coupons.Remove(forDelete);
                await _cont.SaveChangesAsync();               
            }

            return true;
        }

        public async Task<Coupon> GetDiscountAsync(string productname)
        {
            var ent = _cont.Coupons.Where(x => x.ProductName == productname).FirstOrDefault();
            if(ent != null)
            {
                return ent;
            }
            return new Coupon() { Amount=0, ProductName="nodiscount", Description="no discount", Id=0 };
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            var ent = _cont.Coupons.Where(x => x.Id == coupon.Id).FirstOrDefault();
            if(ent != null)
            {
                ent.ProductName = coupon.ProductName;
                ent.Description = coupon.Description;
                ent.Amount = coupon.Amount;
                await _cont.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
