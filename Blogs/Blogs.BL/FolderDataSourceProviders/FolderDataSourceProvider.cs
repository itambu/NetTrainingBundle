using Blogs.BL.Abstractions;
using Blogs.BL.BlogDataSources;
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
    public class FolderDataSourceProvider : IEnumerable<IBlogDataSource<BlogDataSourceDTO>>
    {
        private IConfigurationRoot _config;
        private IFileManager _fileManager;

        public FolderDataSourceProvider(IConfigurationRoot config, IFileManager fileManager)
        {
            _config = config;
            _fileManager = fileManager;
        }

        public IEnumerator<IBlogDataSource<BlogDataSourceDTO>> GetEnumerator()
        {
            var temp = Directory.GetFiles(_config.GetSection("FolderOptions").GetSection("source").Value,
            _config.GetSection("FolderOptions").GetSection("type").Value);
            foreach (var f in temp)
            {
                yield return new BlogDataSource(f, _config, _fileManager);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
