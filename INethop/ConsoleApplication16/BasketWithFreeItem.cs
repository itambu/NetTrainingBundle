using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class BasketWithFreeItem : RegularBasket, IBasket
    {
        public BasketWithFreeItem(ICollection<ISaleItem> items)
            : base(items)
        {
        }

        public new double Total
        {
            get
            {
                ISaleItem last = Items.LastOrDefault();
                return base.Total - last.Total;
            }
        }
    }
}
