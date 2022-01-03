using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.BL.StartApp;
using Blogs.ServiceClient.UniversalClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.ServiceClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IAsyncApp _app;
        private Task _startAppTask;
        private AppOptions _appOptions;
        
        public Worker(ILogger<Worker> logger, IOptions<AppOptions> appOptions)
        {
            _logger = logger;
            _appOptions = appOptions.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Blogs service successfully started");

            _app = new UniversalApp(_appOptions, 
                (sender, ds)=> _logger.LogInformation($"Task on file {ds} failed"),
                (sender, ds) => _logger.LogInformation($"Task on file {ds} completed"),
                (sender, ds) => _logger.LogInformation($"Task on file {ds} interrupted")
            );

            _app.OnStop += (sender, arg) => _logger.LogInformation("Blogs service successfully stopped");
            _app.OnCancel += (sender, arg) => _logger.LogInformation("Service stopped with cancelling of the current tasks");

            return _startAppTask = _app.StartAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_startAppTask != null)
            {
                _startAppTask.Wait();
                if (_app != null)
                {
                    if (!_app.StopAsync().Wait(_appOptions.TimeoutForStop))
                    {
                        _app.CancelAsync().Wait();
                    }
                    _app.Dispose();
                    _app = null;
                }
            }
            return Task.CompletedTask;
        }

    }
}
