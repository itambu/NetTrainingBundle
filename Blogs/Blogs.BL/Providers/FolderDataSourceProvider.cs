using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.IO;

namespace Blogs.BL.Providers
{
    public class FolderDataSourceProvider<DTOEntity> : BaseProvider<DTOEntity>, IDataSourceProvider<DTOEntity>
    {
        public FolderDataSourceProvider(AppFolderOptions appFolderOptions,
            IDataSourceFactory<DTOEntity> dataSourceFactory,
            ActionTokenSet tokenSet) : base(appFolderOptions, dataSourceFactory, tokenSet)
        {
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        protected override void StartAction()
        {
            var temp = Directory.GetFiles(AppFolderOptions.Source, AppFolderOptions.Pattern);
            foreach (var f in temp)
            {
                if (TokenSet.IsStopped || TokenSet.IsCancelled) break;
                OnNew(this, DataSourceFactory.CreateInstance(f));

            }
        }

    }
}
