using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class Order
    {
        public IBasket Basket { get; private set; }
        public DateTime Created { get; private set; }

        public Order(IBasket basket, DateTime created)
        {
            Basket = basket;
            Created = created;
        }


    }
}
