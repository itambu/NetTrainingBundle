using Blogs.BL.Abstractions;
using Blogs.BL.AsyncHandlers;
using Blogs.BL.ConnectionFactories;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.DataSourceHandlers;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.FolderDataSourceProviders;
using Blogs.BL.Infrastructure;
using Blogs.BL.ProcessManagers;
using Blogs.DAL.Abstractions;
using Blogs.DAL.BlogContextFactories;
using Blogs.DAL.RepositotyFactories;
using Blogs.Persistence.Contexts;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    public class ConfiguredApp : BaseApp, IAsyncApp
    {
        protected IProcessHandler<BlogDataSourceDTO> _folderManager;
        protected IProcessHandler<BlogDataSourceDTO> _eventedManager;
        protected EntityConcurrencyHandler _entityConcurrencyHandler = new EntityConcurrencyHandler();
        protected FileSystemWatcher Watcher;
        protected IConnectionFactory connectionFactory;
        protected IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory;
        protected IBlogContextFactory contextFactory;
        protected IRepositoryFactory repoFactory;
        protected IDataSourceHandleBuilder<BlogDataSourceDTO> dataSourceHandlerBuilder;

        public ConfiguredApp(AppOptions appOptions) : base(appOptions)
        {
            //InitConfig();
            InitWatcher();
            InitConnectionFactory();
            InitDataSourceFactory();
            InitDbContextFactory();
            InitRepositoryFactory();
            EnsureDataBase();
            
            InitDataSourceHandlerBuilder();
            InitManagers();
            InitParallelismHandlers();
            Configure();
        }

        #region Configuration

        protected virtual void EnsureDataBase()
        {
            using (var context = new BlogDbContext(connectionFactory.CreateInstance(), true))
            {
                context.Database.CreateIfNotExists();
            }
        }

        protected virtual void InitDataSourceHandlerBuilder()
        {
            dataSourceHandlerBuilder = new BlogDataSourceHandlerBuilder()
            {
                ConnectionFactory = connectionFactory,
                ContextFactory = contextFactory,
                ReposFactory = repoFactory,
                HandlerFactory = new BlogDataSourceHandlerFactory(
                    new ConsistencyHandler(connectionFactory, contextFactory, repoFactory)),
                CancelTokenSource = TokenSources.Cancel,
                ParserFactory = new BlogDTOParserFactory(),
                EntityConcurrencyHandler = _entityConcurrencyHandler 
            };
        }

        protected virtual void InitRepositoryFactory()
        {
            repoFactory = new RepositoryFactory();
        }

        protected virtual void InitDbContextFactory()
        {
            contextFactory = new BlogContextFactory();
        }

        protected virtual void InitDataSourceFactory()
        {
            dataSourceFactory = new DataSourceFactory(_appOptions.FolderOptions);
        }

        protected virtual void InitConnectionFactory()
        {
            connectionFactory = new SqlConnectionFactory(_appOptions.ConnectionOptions.Default);
        }

        protected virtual void InitManagers()
        {
            _folderManager = new FolderManager<BlogDataSourceDTO>(
                dataSourceHandleBuilder: dataSourceHandlerBuilder,
                provider: new FolderDataSourceProvider(_appOptions.FolderOptions, new TxFileManager()),
                tokens : TokenSources.Tokens
                );

            _eventedManager = new EventedFileManager<BlogDataSourceDTO>(
                dataSourceHandleBuilder:  dataSourceHandlerBuilder,
                tokens: TokenSources.Tokens,
                dataSourceFactory: dataSourceFactory,
                watcher: Watcher
                );
        }

        protected virtual void InitParallelismHandlers()
        {
            AsyncHandlers.Add(typeof(FolderManager<BlogDataSourceDTO>),
                new BaseAsyncHandler<BlogDataSourceDTO>
                    (_folderManager, 
                    new ConcurrentBag<Task>(),
                    TokenSources.Tokens,
                    new MonitorLocker(),
                    TaskScheduler.Default
                    ));
            AsyncHandlers.Add(typeof(EventedFileManager<BlogDataSourceDTO>),
                new BaseAsyncHandler<BlogDataSourceDTO>
                    (_eventedManager,
                    new ConcurrentBag<Task>(),
                    TokenSources.Tokens,
                    new MonitorLocker(),
                    TaskScheduler.Default
                    ));
        }

        protected virtual void InitWatcher()
        {
            Watcher = new FileSystemWatcher(
                _appOptions.FolderOptions.Source,
                _appOptions.FolderOptions.Pattern
                );
        }

        //protected virtual void InitConfig()
        //{
        //    //_config = (new ConfigurationBuilder())
        //    //    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
        //    //    .AddJsonFile("appsettings.json").Build();
        //}

        protected virtual void Configure()
        {
            this.OnStop += (_eventedManager as EventedFileManager<BlogDataSourceDTO>).OnStopHandler;
        }

        #endregion

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (_entityConcurrencyHandler != null)
                {
                    _entityConcurrencyHandler.Dispose();
                    _entityConcurrencyHandler = null;
                }
                if (Watcher != null)
                {
                    Watcher.Dispose();
                    Watcher = null;
                }
                if (_eventedManager != null) 
                { 
                    (_eventedManager as IDisposable).Dispose();
                    _eventedManager = null;
                }
            }
            base.Dispose(isDisposing);
        }

        ~ConfiguredApp()
        {
            Dispose();
        }
    }
}
