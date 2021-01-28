using INetShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.BL
{
    public class RegularBasket: Basket, IBasket
    {
        public RegularBasket(ICollection<IBasketItem> items) : base(items)
        {
        }

        public double TotalCost
        {
            get
            {
                if (Items != null)
                {
                    double s = 0;
                    foreach (ITotalCostCalculable item in Items)
                    {
                        s += item.TotalCost;
                    }
                    return s;
                }
                else
                {
                    throw new NullReferenceException("Property Items cannot be null");
                }
            }
        }
    }
}
