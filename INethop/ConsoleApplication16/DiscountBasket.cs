using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class DiscountBasket : RegularBasket, IBasket
    {
        public DiscountBasket(ICollection<ISaleItem> items)
            : base(items)
        {
        }

        public new double Total
        {
            get
            {
                return base.Total * 0.9;
            }
        }
    }
}
