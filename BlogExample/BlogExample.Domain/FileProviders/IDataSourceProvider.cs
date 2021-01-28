using BlogExample.BL.Absractions;
using BlogExample.BL.CSVParsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.FileProviders
{
    public interface IDataSourceProvider<T> : IDisposable, IProcessHandler
    {
        event EventHandler<IDataSource<T>> New;
    }
}
