using Blogs.BL.Abstractions;
using Blogs.BL.BlogDataSources;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.DataSourceFactories
{
    public class DataSourceFactory : IDataSourceFactory<BlogDataSourceDTO>
    {
        private IConfigurationRoot _config;
        public IBlogDataSource<BlogDataSourceDTO> CreateInstance(string fileName)
        {
            return new BlogDataSource(fileName, _config, new TxFileManager());
        }

        public DataSourceFactory(IConfigurationRoot config)
        {
            _config = config;
        }
    }
}
