using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INetShop.Entities
{
    public interface IBasket : ITotalCostCalculable
    {
        ICollection<IBasketItem> Items { get; }
    }
}
