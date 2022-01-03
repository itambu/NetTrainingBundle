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
        private IFileManager _fileManager;

        public FolderDataSourceProvider(AppFolderOptions appFolderOptions, IFileManager fileManager)
        {
            _appFolderOptions = appFolderOptions;
            _fileManager = fileManager;
        }

        public IEnumerator<IDataSource<BlogDataSourceDTO>> GetEnumerator()
        {
            var temp = Directory.GetFiles(_appFolderOptions.Source,_appFolderOptions.Pattern);
            foreach (var f in temp)
            {
                yield return new BlogDataSource(f, _appFolderOptions.Target, _fileManager);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
