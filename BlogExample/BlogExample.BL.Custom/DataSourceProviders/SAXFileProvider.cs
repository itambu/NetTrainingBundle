using BlogExample.BL.CSVParsing;
using BlogExample.BL.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.DataSourceProviders
{
    public class SAXFileProvider : BaseFileProvider<CSVDTO>, IDataSourceProvider<CSVDTO>
    {
        bool _isCancelled = false;

        public SAXFileProvider(): base()
        {
        }
        public SAXFileProvider(string sourceFolder, string destFolder) 
            : base(sourceFolder, destFolder)
        {
        }

        public void Cancel()
        {
            _isCancelled = false;
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public void Start()
        {
            foreach (var c in
            Directory.GetFiles(
                SourceFolder,
                SearchPattern,
                SearchOption.TopDirectoryOnly))
            {
                OnNew(this, new CsvDTOParser(c, this.DestFolder));
                if (_isCancelled)
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            Cancel();
        }

    }
}
