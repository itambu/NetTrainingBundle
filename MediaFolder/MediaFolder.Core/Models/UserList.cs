using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFolder.Core.Models
{
    class UserList : Element, IMediaList
    {
        public ICollection<IElement> Items{ get; private set;}
        public UserList(Guid id, string name, ICollection<IElement> items)
            : base(id, name)
        {
            Items = items;
        }
    }
}
