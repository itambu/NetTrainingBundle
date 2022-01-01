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
    public class App : IAsyncApp, IDisposable
    {
        private bool isDisposed = false;

        protected IProcessHandler<BlogDataSourceDTO> _folderManager;
        protected IProcessHandler<BlogDataSourceDTO> _eventedManager;

        protected IConfigurationRoot _config;
        protected TokenSourceSet TokenSources = new TokenSourceSet(stop: new CancellationTokenSource(), cancel: new CancellationTokenSource());
        protected EntityConcurrencyHandler _entityConcurrencyHandler = new EntityConcurrencyHandler();


        protected FileSystemWatcher Watcher;
        protected IDictionary<Type, IAsyncHandler<BlogDataSourceDTO>> AsyncHandlers = new Dictionary<Type, IAsyncHandler<BlogDataSourceDTO>>();
        protected IConnectionFactory connectionFactory;
        protected IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory;
        protected IBlogContextFactory contextFactory;
        protected IRepositoryFactory repoFactory;
        protected IDataSourceHandleBuilder<BlogDataSourceDTO> dataSourceHandlerBuilder;

        public event EventHandler OnStop;
        public event EventHandler OnCancel;

        public App()
        {
            InitConfig();
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
            using (var context = new BlogDbContext(connectionFactory.CreateInstance(), false))
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
            dataSourceFactory = new DataSourceFactory(_config);
        }

        protected virtual void InitConnectionFactory()
        {
            connectionFactory = new SqlConnectionFactory(_config);
        }

        protected virtual void InitManagers()
        {
            _folderManager = new FolderManager<BlogDataSourceDTO>(
                dataSourceHandleBuilder: dataSourceHandlerBuilder,
                provider: new FolderDataSourceProvider(_config, new TxFileManager()),
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
                _config.GetSection("FolderOptions").GetSection("source").Value,
                _config.GetSection("FolderOptions").GetSection("type").Value
                );
        }

        protected virtual void InitConfig()
        {
            _config = (new ConfigurationBuilder())
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        protected virtual void Configure()
        {
            this.OnStop += (_eventedManager as EventedFileManager<BlogDataSourceDTO>).OnStopHandler;
        }

        #endregion 
        public Task Pause()
        {
            throw new NotImplementedException();
        }

        public Task Resume()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnStopEvent(object sender, EventArgs args)
        {
            TokenSources.Stop.Cancel();
            OnStop?.Invoke(this, args);
        }

        protected virtual void OnCancelEvent(object sender, EventArgs args)
        {
            OnStop?.Invoke(this, args);
        }

        public Task StartAsync()
        {
            return Task.WhenAll(AsyncHandlers.Values.Select(x => x.StartMainProcess()));
        }

        public Task StopAsync()
        {
            OnStopEvent(this, null);
            var temp = AsyncHandlers.Values;
            return Task.WhenAll(temp.Select(x => x.WhenAll()).Concat(temp.Select(x => x.WhenMainProcess())) );
        }

        public void Dispose()
        {
            if (isDisposed) return;

            if (_entityConcurrencyHandler != null) _entityConcurrencyHandler.Dispose();
            if (TokenSources!=null) TokenSources.Dispose();
            if (Watcher != null) Watcher.Dispose();
            if (_eventedManager != null) { (_eventedManager as IDisposable).Dispose(); }
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        ~App()
        {
            Dispose();
        }

        public Task CancelAsync()
        {
            TokenSources.Stop.Cancel();
            TokenSources.Cancel.Cancel();
            OnCancelEvent(this, null);
            var temp = AsyncHandlers.Values;
            return Task.WhenAll(temp.Select(x => x.WhenAll()).Concat(temp.Select(x => x.WhenMainProcess())));
        }
    }
}
