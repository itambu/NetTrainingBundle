using ConsoleApplication16.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class Item : IElement
    {
        public int Id{ get; private set;}
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Item(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
