using INetShop.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.Entities.Classes
{
    public class OrderHistoryItem : HistoryItem<IOrder, OrderStatus>
    {
        public OrderHistoryItem(int id, IOrder order, DateTime changed, OrderStatus status)
            : base(id, order, changed, status)
        {
        }
    }
}
