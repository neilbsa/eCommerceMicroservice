using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            Username = username;
        }



        public decimal TotalPrice
        {
            get
            {
                decimal currentVal = 0;
                if (ShoppingCartItems != null)
                {
                    currentVal =this.ShoppingCartItems.Sum(x => x.Quantity * x.Price);
                }
                return currentVal;
            }
        }
    }
}
