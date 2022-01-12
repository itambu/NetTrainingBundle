using Blogs.BL.Abstractions;
using Blogs.BL.BlogDataSources;
using Blogs.BL.Infrastructure;
using ChinhDo.Transactions;

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
