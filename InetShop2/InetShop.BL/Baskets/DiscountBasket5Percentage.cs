using INetShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.BL
{
    public class DiscountBasket5Percentage : RegularBasket, ITotalCostCalculable
    {
        public DiscountBasket5Percentage(ICollection<IBasketItem> items) : base(items)
        { 
        }

        public new double TotalCost
        {
            get
            {
                return base.TotalCost * 0.95;
            }
        }
    }
}
