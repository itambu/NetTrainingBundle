using INetShop.BL.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INetShop.Entities.Classes;
using INetShop.Entities.Interfaces;

namespace INetShop.BL.Unity
{
    public class Unity 
    {
        public ElementRepository ElementRepository { get; private set; }
        public ICollection<HistoryItem<IOrder, OrderStatus>>OrderHistoryRepository{ get;set;}

        public Unity(ElementRepository elementRepository)
        {
            ElementRepository = elementRepository;
        }
    }
}
