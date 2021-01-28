using INetShop.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.Entities
{
    public class Item : IElement
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }

        public Item(int id, string name)
        {
            Id = id;
            Name = name; 
        }
    }
}
