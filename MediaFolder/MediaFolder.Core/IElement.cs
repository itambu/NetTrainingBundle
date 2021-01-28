using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaFolder.Core
{
    public interface IElement : INameable
    {
        Guid Id { get; }
    }
}
