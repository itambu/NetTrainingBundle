﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16.Interfaces
{
    public interface ISaleItem : ITotalable
    {
        IElement Item { get; }
    }
}
