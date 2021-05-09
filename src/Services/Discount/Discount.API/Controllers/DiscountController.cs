using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase  
    {
        private readonly IDiscountRepository _rep;
        public DiscountController(IDiscountRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }

        [HttpGet("{productname}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productname)
        {
            var discount = await _rep.GetDiscountAsync(productname);
            return Ok(discount);
        }



        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody]Coupon coupon)
        {
            _ = await _rep.CreateDiscountAsync(coupon);
            return CreatedAtAction("GetDiscount", new { productname = coupon.ProductName }, coupon);
        }



        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
          
            return Ok(await _rep.UpdateDiscountAsync(coupon));
        }


        [HttpDelete("{productname}", Name ="DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount(string productname)
        {

            return Ok(await _rep.DeleteDiscountAsync(productname));
        }



    }
}
