using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaFolder.Core
{
    public interface IMediaList : INameable
    {
        Guid Id { get; }
        ICollection<IElement> Items { get; }
    }
}
