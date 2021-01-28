using System;
using System.Collections.Generic;
using System.IO;

namespace BlogExample.BL.CSVParsing
{
    public interface IDataSource<T> : IEnumerable<T>, IDisposable
    {
        void Close();
    }
}