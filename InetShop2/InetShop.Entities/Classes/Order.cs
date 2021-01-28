using INetShop.Entities;
using INetShop.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.Entities
{
    public class Order : IOrder
    {
        public IBasket Basket { get; private set; }
        public DateTime OrderSubmitted { get; private set; }

        public Order(IBasket basket, DateTime orderSubmitted)
        {
            Basket = basket;
            OrderSubmitted = orderSubmitted;
        }
    }
}
