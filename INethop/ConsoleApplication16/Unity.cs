using ConsoleApplication16.GenericRepository;
using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class Unity
    {
        public ICollection<IElement> Items { get; private set;}
        public IGenericRepository<Order> Orders { get; private set; }
        public ICollection<User> Users { get; private set; }

        public Unity(ICollection<IElement> items, IGenericRepository<Order> orders, ICollection<User> users)
        {
            Items = items;
            Orders = orders;
            Users = users;
        }

        
        public void OnOrderAdd(object sender, Order order)
        {
            //
        }

    }
}
