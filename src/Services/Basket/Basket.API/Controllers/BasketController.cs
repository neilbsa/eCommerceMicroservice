using Basket.API.Entities;
using Basket.API.GRPC;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;
        private readonly DiscountGRPC discountGRPC;

        public BasketController(IBasketRepository basket, DiscountGRPC discountGRPC)
        {
            _basket = basket ?? throw new ArgumentNullException(nameof(basket));
            this.discountGRPC = discountGRPC ?? throw new ArgumentNullException(nameof(discountGRPC));
        }

        [HttpGet("{username}",Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasketAsync(string username)
        {
            var basket = await _basket.GetBasketsAsync(username);
            return Ok(basket ?? new ShoppingCart(username));
        }




        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasketAsync([FromBody] ShoppingCart cart)
        {


            //TODO: comunicate discount GRPC and
            foreach (var item in cart.ShoppingCartItems)
            {
                var coupon = await discountGRPC.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            //calculate latest prices of products into shopping cart
            var basket = await _basket.UpdateBasketAsync(cart);
            return Ok(cart);
        }




        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> DeleteBasketAsync(string username)
        {
            await _basket.DeleteBasketAsync(username);
            return Ok();
        }
    }
}
