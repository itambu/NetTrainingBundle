using Blogs.BL.Abstractions;
using Blogs.BL.ConnectionFactories;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.DataSourceHandlers;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.FolderDataSourceProviders;
using Blogs.BL.ParallelismHandlers;
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
    public class App : IControlable, IDisposable
    {
        protected IFileProcessManager<BlogDataSourceDTO> _folderManager;
        protected IFileProcessManager<BlogDataSourceDTO> _eventedManager;

        IConfigurationRoot _config;
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationTokenSource stopTokenSource = new CancellationTokenSource();
        FileSystemWatcher Watcher;
        IDictionary<Type, IParallelismHandler> ParallelismHandlers = new Dictionary<Type, IParallelismHandler>();
        IConnectionFactory connectionFactory;
        IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory;
        IBlogContextFactory contextFactory;
        IRepositoryFactory repoFactory;
        IDataSourceHandleBuilder<BlogDataSourceDTO> dataSourceHandlerBuilder;

        private bool isDisposed = false;

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
            InitParallelismHandlers();
            InitManagers();
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
                CancelTokenSource = cancelTokenSource,
                ParserFactory = new BlogDTOParserFactory()
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
                dataSourceHandlerBuilder,
                new FolderDataSourceProvider(_config, new TxFileManager()),
                ParallelismHandlers[typeof(FolderManager<BlogDataSourceDTO>)]
                );

            _eventedManager = new EventedFileManager<BlogDataSourceDTO>(
                dataSourceHandlerBuilder,
                ParallelismHandlers[typeof(EventedFileManager<BlogDataSourceDTO>)],
                Watcher, 
                dataSourceFactory
                );
        }

        protected virtual void InitParallelismHandlers()
        {
            ParallelismHandlers.Add(typeof(FolderManager<BlogDataSourceDTO>),
                new ParallelismHandler
                    (
                    cancelTokenSource,
                    stopTokenSource,
                    TaskScheduler.Default,
                    new ConcurrentBag<Task>()));
            ParallelismHandlers.Add(typeof(EventedFileManager<BlogDataSourceDTO>),
                new ParallelismHandler
                    (
                    cancelTokenSource,
                    stopTokenSource,
                    TaskScheduler.Default,
                    new ConcurrentBag<Task>()));
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
            _folderManager.TaskFailed += (obj, ds) => { Console.WriteLine("Failed"); };
            _folderManager.TaskCompleted += (obj, ds) => { Console.WriteLine("Completed"); };

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
            OnStop?.Invoke(this, args);
        }

        protected virtual void OnCancelEvent(object sender, EventArgs args)
        {
            OnStop?.Invoke(this, args);
        }

        public async Task Start()
        {
            await Task.WhenAll(_eventedManager.Run(), _folderManager.Run());
        }

        public async Task Stop()
        {
            OnStopEvent(this, null);
            Task.WhenAll(ParallelismHandlers.Values.Select(x => x.RequestStop())).Wait();
            await Task.WhenAll(ParallelismHandlers.Values.Select(x => x.WaitForCompletion()));
        }

        public void Dispose()
        {
            if (isDisposed) return;
            if (cancelTokenSource!=null) cancelTokenSource.Dispose();
            if (stopTokenSource != null) stopTokenSource.Dispose();
            if (Watcher != null) Watcher.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public async Task Cancel()
        {
            OnCancelEvent(this, null);
            await ParallelismHandlers[typeof(EventedFileManager<BlogDataSourceDTO>)].RequestCancel();
            await ParallelismHandlers[typeof(FolderManager<BlogDataSourceDTO>)].RequestCancel();
        }
    }
}
