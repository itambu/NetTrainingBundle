﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salad.BL.Interfaces
{
    public interface ISaladItem
    {
        double Weight { get; }
        
        string Name { get; }
    }
}
