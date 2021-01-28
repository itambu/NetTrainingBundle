using BlogExample.BL.CSVParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogExample.BL.LogicTaskContexts
{
    public class LogicTaskContext<TDataItem>
    {
        public CancellationToken CancellationToken { get; set; }
        public Task Current { get; set; }
        public Exception Exception { get; set; }
        public IDataSource<TDataItem> DataSource { get; set; }

        public TDataItem DataItem { get; set; }
    }
}
