using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public abstract class Basket
    {
        public ICollection<ISaleItem> Items { get; private set; }
        public Basket(ICollection<ISaleItem> items)
        {
            Items = items;
        }
    }
}
