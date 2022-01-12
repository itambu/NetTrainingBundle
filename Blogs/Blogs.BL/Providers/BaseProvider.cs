using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Threading;

namespace Blogs.BL.Providers
{
    public abstract class BaseProvider<DTOEntity>
    {
        protected AppFolderOptions AppFolderOptions { get; private set; }
        protected IDataSourceFactory<DTOEntity> DataSourceFactory { get; private set; }
        protected ActionTokenSet TokenSet { get; private set; }

        public event EventHandler<IDataSource<DTOEntity>> New;

        public BaseProvider(
            AppFolderOptions appFolderOptions,
            IDataSourceFactory<DTOEntity> dataSourceFactory,
            ActionTokenSet tokenSet)
        {
            AppFolderOptions = appFolderOptions;
            DataSourceFactory = dataSourceFactory;
            TokenSet = tokenSet;
        }
        public void Start()
        {
            StartAction();
        }
        protected abstract void StartAction();
        protected virtual void OnNew(object sender, IDataSource<DTOEntity> dataSource)
        {
            var temp = New;
            Interlocked.Exchange(ref temp, New);
            temp?.Invoke(sender, dataSource);
        }

    }
}
