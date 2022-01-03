using Blogs.BL.Abstractions;
using Blogs.BL.BlogDataSources;
using Blogs.BL.Infrastructure;
using ChinhDo.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.DataSourceFactories
{
    public class DataSourceFactory : IDataSourceFactory<BlogDataSourceDTO>
    {
        private AppFolderOptions _folderOptions;
        public IDataSource<BlogDataSourceDTO> CreateInstance(string fileName)
        {
            return new BlogDataSource(fileName, _folderOptions.Target, new TxFileManager());
        }

        public DataSourceFactory(AppFolderOptions folderOptions)
        {
            _folderOptions = folderOptions;
        }
    }
}
