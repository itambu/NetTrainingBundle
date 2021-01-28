using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFolder.Core.Models
{
    public class Mediateka
    {
        public ICollection<IElement> Items { get; private set; }
        public ICollection<IMediaList> PlayLists { get; private set; }

        public Mediateka(ICollection<IElement> items, ICollection<IMediaList> userLists)
        {
            Items = items;
            PlayLists = userLists;
        }

        
    }
}
