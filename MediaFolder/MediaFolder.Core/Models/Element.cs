using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFolder.Core.Models
{
    public abstract class  Element : IElement 
    {
        public Guid Id{ get; private set;}
        public string Name { get; set; }

        public Element(Guid id, string name)
        {
            Id = id;
            Name = name;
        }


    }
}
