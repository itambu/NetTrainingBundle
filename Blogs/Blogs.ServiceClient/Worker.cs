using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.AsyncAdapters;
using Blogs.BL.ConnectionFactories;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.ControlPanels;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.DataSourceHandlers;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.Infrastructure;
using Blogs.BL.DataSourceHandlerAdapters;
using Blogs.BL.ProcessManagers;
using Blogs.BL.Providers;
using Blogs.BL.StartApp;
using Blogs.DAL.Abstractions;
using Blogs.DAL.BlogContextFactories;
using Blogs.DAL.RepositotyFactories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.ServiceClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IAsyncApp<BlogDataSourceDTO> _app;
        private Task _startAppTask;
        private AppOptions _appOptions;

        EntityConcurrencyHandler _entityConcurrencyHandler;

        public Worker(ILogger<Worker> logger, IOptions<AppOptions> appOptions)
        {
            _logger = logger;
            _appOptions = appOptions.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApp();
            _app.Started += (sender, arg) => _logger.LogInformation("Blogs service successfully started");
            _app.Stopped += (sender, arg) => _logger.LogInformation("Blogs service successfully stopped");
            _app.Cancelled += (sender, arg) => _logger.LogInformation("Service stopped with cancelling of the current tasks");
            return _startAppTask = _app.Start();
        }

        private void InitializeApp()
        {
            IConnectionFactory connectionFactory = new SqlConnectionFactory(_appOptions.ConnectionOptions.Default);
            IBlogContextFactory contextFactory = new BlogContextFactory(connectionFactory);

            IRepositoryFactory repoFactory = new RepositoryFactory();
            IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory = new DataSourceFactory(_appOptions.FolderOptions);
            IDTOParserFactory<BlogDataSourceDTO> parserFactory = new BlogDTOParserFactory();
            _entityConcurrencyHandler = new EntityConcurrencyHandler();

            IAsyncControlPanel<BlogDataSourceDTO> controlPanel = new AsyncControlPanel<BlogDataSourceDTO>(
                new List<IAsyncAdapter<BlogDataSourceDTO>>(),
                new TokenSourceSet(
                    new CancellationTokenSource(),
                    new CancellationTokenSource()
                    ),
                _appOptions.TimeoutForStop
                );

            IDataItemHandlerFactory<BlogDataSourceDTO> dataItemHandlerFactory = new DataItemHandlerFactory(
                contextFactory,
                repoFactory,
                parserFactory,
                _entityConcurrencyHandler);

            IDataSourceHandlerFactory<BlogDataSourceDTO> dataSourceHandlerFactory = new BlogDataSourceHandlerFactory(
                dataItemHandlerFactory,
                new ConsistencyHandler(contextFactory, repoFactory),
                controlPanel.TokenSources.Tokens.Cancel);

            AbstractDataSourceAdapterFactory<BlogDataSourceDTO, BaseDataSourceAdapter<BlogDataSourceDTO>> adapterFactory
                = new AbstractDataSourceAdapterFactory<BlogDataSourceDTO, BaseDataSourceAdapter<BlogDataSourceDTO>>(
                    dataSourceHandlerFactory,
                    forCompleted: (sender, ds) => _logger.LogInformation($"Task on file {ds} completed"),
                    forFailed: (sender, ds) => _logger.LogInformation($"Task on file {ds} failed"),
                    forInterrupted: (sender, ds) => _logger.LogInformation($"Task on file {ds} interrupted")
                    );

            var folderManager = adapterFactory.CreateInstance();

            var eventedManager = adapterFactory.CreateInstance();

            FolderDataSourceProvider<BlogDataSourceDTO> folderProvider
                = new FolderDataSourceProvider<BlogDataSourceDTO>(
                    _appOptions.FolderOptions,
                    dataSourceFactory,
                    controlPanel.TokenSources.Tokens);

            EventedProvider<BlogDataSourceDTO> eventedProvider
                = new EventedProvider<BlogDataSourceDTO>(
                    _appOptions.FolderOptions,
                    dataSourceFactory,
                    controlPanel.TokenSources.Tokens
                    );

            IAsyncAdapter<BlogDataSourceDTO> _folderAsyncHandler =
                new BaseAsyncAdapter<BlogDataSourceDTO>(
                    folderManager,
                    new AsyncAdapterOptions()
                    {
                        Tokens = controlPanel.TokenSources.Tokens,
                        TaskCollection = new ConcurrentBag<Task>()
                    },
                    folderProvider
                    );


            IAsyncAdapter<BlogDataSourceDTO> _eventedAsyncHandler =
                new BaseAsyncAdapter<BlogDataSourceDTO>
                    (eventedManager,
                    new AsyncAdapterOptions()
                    {
                        Tokens = controlPanel.TokenSources.Tokens,
                        TaskCollection = new ConcurrentBag<Task>()
                    },
                    eventedProvider);

            folderProvider.New += _folderAsyncHandler.PendingTask;
            eventedProvider.New += _eventedAsyncHandler.PendingTask;

            controlPanel.StopRequested += (sender, args) => { eventedProvider.Stop(); };

            controlPanel
                .Add(_eventedAsyncHandler)
                .Add(_folderAsyncHandler);
                
            _app = new BaseAsyncApp<BlogDataSourceDTO>(controlPanel, contextFactory, _appOptions);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_startAppTask != null)
            {
                _startAppTask.Wait();
                if (_app != null)
                {
                    _app.Stop().Wait();
                    _entityConcurrencyHandler.Dispose();
                    _app.Dispose();
                    _app = null;
                }
            }
            return Task.CompletedTask;
        }

    }
}
