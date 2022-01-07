using Blogs.BL.Abstractions;
using Blogs.BL.BlogDataSources;
using Blogs.BL.Infrastructure;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.FolderDataSourceProviders
{
    public class FolderDataSourceProvider : IEnumerable<IDataSource<BlogDataSourceDTO>>
    {
        private AppFolderOptions _appFolderOptions;
        private IDataSourceFactory<BlogDataSourceDTO> _dataSourceFactory;
        public FolderDataSourceProvider(AppFolderOptions appFolderOptions, IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory)
        {
            _appFolderOptions = appFolderOptions;
            _dataSourceFactory = dataSourceFactory;
        }

        public IEnumerator<IDataSource<BlogDataSourceDTO>> GetEnumerator()
        {
            var temp = Directory.GetFiles(_appFolderOptions.Source,_appFolderOptions.Pattern);
            foreach (var f in temp)
            {
                yield return _dataSourceFactory.CreateInstance(f);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
