using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaFolder.Core
{
    public interface IPlayable
    {
        void Play(IPlayer player);
    }
}
