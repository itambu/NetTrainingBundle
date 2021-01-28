using INetShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INetShop.Entities
{
    public abstract class Basket
    {
        public ICollection<IBasketItem> Items { get; private set; }

        public Basket(ICollection<IBasketItem> items)
        {
            Items = items;
        }
    }
}
