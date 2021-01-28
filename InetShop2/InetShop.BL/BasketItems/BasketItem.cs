using INetShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.BL.BasketItems
{
    public class BasketItem : IBasketItem
    {
        public Item Item { get; private set; }
        public double Amount { get; set; }
        public double CostPerItem { get; set; }

        public double TotalCost
        {
            get { return Amount * CostPerItem; }
        }

        public BasketItem(Item item)
        {
            Item = item;
        }
    }
}
