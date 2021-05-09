using Discount.API.Entities;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Data
{
    public class DiscountContextSeed
    {

        public static async Task SeedAsync(DiscountContext discount, ILogger<DiscountContextSeed> logger)
        {

         
        
                if (!discount.Coupons.Any())
                {
                    discount.Coupons.AddRange(GetPreconfiguredOrders());
                    await discount.SaveChangesAsync();
                    logger.LogInformation("Seed database associated with context");
                }
           

           
        }

        private static IEnumerable<Coupon> GetPreconfiguredOrders()
        {
            return new List<Coupon>()
            {
                new Coupon() { Amount= 0, Description="NULL", ProductName="Name"}
            };
        }


    }
}
