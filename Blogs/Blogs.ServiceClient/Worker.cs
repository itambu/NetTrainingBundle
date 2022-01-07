using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.AsyncHandlers;
using Blogs.BL.ConnectionFactories;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.DataSourceHandlers;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.FolderDataSourceProviders;
using Blogs.BL.Infrastructure;
using Blogs.BL.ProcessManagers;
using Blogs.BL.StartApp;
using Blogs.DAL.Abstractions;
using Blogs.DAL.BlogContextFactories;
using Blogs.DAL.RepositotyFactories;
using Blogs.ServiceClient.UniversalClient;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        
        public Worker(ILogger<Worker> logger, IOptions<AppOptions> appOptions)
        {
            _logger = logger;
            _appOptions = appOptions.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApp();
            _app.TaskFailed += (sender, ds) => _logger.LogInformation($"Task on file {ds} failed");
            _app.TaskCompleted += (sender, ds) => _logger.LogInformation($"Task on file {ds} completed");
            _app.TaskInterrupted += (sender, ds) => _logger.LogInformation($"Task on file {ds} interrupted");
            _app.OnStopped += (sender, arg) => _logger.LogInformation("Blogs service successfully stopped");
            _app.OnCancelled += (sender, arg) => _logger.LogInformation("Service stopped with cancelling of the current tasks");

            _startAppTask = _app.StartAsync();
            _logger.LogInformation("Blogs service successfully started");
            return _startAppTask;
        }

        private void InitializeApp()
        {
            IConnectionFactory connectionFactory = new SqlConnectionFactory(_appOptions.ConnectionOptions.Default);
            IBlogContextFactory contextFactory = new BlogContextFactory(connectionFactory);

            IRepositoryFactory repoFactory = new RepositoryFactory();
            IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory = new DataSourceFactory(_appOptions.FolderOptions);
            IDTOParserFactory<BlogDataSourceDTO> parserFactory = new BlogDTOParserFactory();
            EntityConcurrencyHandler entityConcurrencyHandler = new EntityConcurrencyHandler();

            IAsyncControlPanel<BlogDataSourceDTO> controlPanel = new AsyncControlPanel<BlogDataSourceDTO>(
                new List<IAsyncHandler<BlogDataSourceDTO>>(),
                new TokenSourceSet(
                    new CancellationTokenSource(),
                    new CancellationTokenSource()
                    ),
                 new TaskBlocker(
                     new FileSystemWatcher(_appOptions.FolderOptions.Source, _appOptions.FolderOptions.Pattern)),
                _appOptions.TimeoutForStop
                );

            IDataItemHandlerFactory<BlogDataSourceDTO> dataItemHandlerFactory = new DataItemHandlerFactory(
                contextFactory,
                repoFactory,
                parserFactory,
                entityConcurrencyHandler);

            IDataSourceHandlerFactory<BlogDataSourceDTO> dataSourceHandlerFactory = new BlogDataSourceHandlerFactory(
                dataItemHandlerFactory,
                new ConsistencyHandler(contextFactory, repoFactory),
                controlPanel.TokenSources.Tokens.Cancel);

            var folderManager = new FolderManager<BlogDataSourceDTO>(
                 dataSourceHandlerFactory: dataSourceHandlerFactory,
                 provider: new FolderDataSourceProvider(_appOptions.FolderOptions, dataSourceFactory),
                 tokens: controlPanel.TokenSources.Tokens
                 );

            var eventedManager = new EventedFileManager<BlogDataSourceDTO>(
                dataSourceHandlerFactory: dataSourceHandlerFactory,
                tokens: controlPanel.TokenSources.Tokens,
                dataSourceFactory: dataSourceFactory                
                );

            IAsyncHandler<BlogDataSourceDTO> _folderAsyncHandler =
                new BaseAsyncHandler<BlogDataSourceDTO>
                    (folderManager, new AsyncHandlerOptions()
                    {
                        Tokens = controlPanel.TokenSources.Tokens,
                        TaskCollection = new ConcurrentBag<Task>()
                    });

            IAsyncHandler<BlogDataSourceDTO>  _eventedAsyncHandler =
                new BaseAsyncHandler<BlogDataSourceDTO>
                    (eventedManager,
                    new AsyncHandlerOptions()
                    {
                        Tokens = controlPanel.TokenSources.Tokens,
                        TaskCollection = new ConcurrentBag<Task>()
                    });
            eventedManager.Bind(controlPanel.TaskBlocker);

            controlPanel
                .Add(_eventedAsyncHandler)
                .Add(_folderAsyncHandler)
                ;
            _app = new BaseAsyncApp<BlogDataSourceDTO>(controlPanel, contextFactory, _appOptions);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_startAppTask != null)
            {
                _startAppTask.Wait();
                if (_app != null)
                {
                    _app.StopAsync().Wait();
                    _app.Dispose();
                    _app = null;
                }
            }
            return Task.CompletedTask;
        }

    }
}
