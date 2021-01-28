using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class SaleItem : ISaleItem
    {
        public IElement Item { get; private set; }
        public double CostPerItem { get; private set; }
        public double Amount { get; private set; }

        public SaleItem(IElement item, double costPerItem, double amount)
        {
            Item = item;
            CostPerItem = costPerItem;
            Amount = amount;
        }

        public double Total
        {
            get { return CostPerItem * Amount; }
        }
    }
}
