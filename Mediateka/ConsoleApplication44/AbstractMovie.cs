﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication44
{
    public abstract class AbstractMovie : MediaItem, IMovieItem
    {
        public TimeSpan Duration { get; set; }
    }
}
