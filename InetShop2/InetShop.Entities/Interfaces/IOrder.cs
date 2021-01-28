using INetShop.Entities.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.Entities.Interfaces
{
    public interface IOrder
    {
        IBasket Basket { get; }
    }
}
