using Blogs.BL.Abstractions;
using Blogs.BL.StartApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Trace.WriteLine("starting...");
            _logger.LogInformation("Blogs service successfully started ku-ku");

            _app = new ConfiguredApp();
            _app.OnStop += (sender, arg) => _logger.LogInformation("Successfully stopped");
            return _startAppTask = _app.StartAsync();
            //return Task.WhenAll(temp, _startAppTask);
            //return base.StartAsync(stoppingToken);
            //return Task.CompletedTask;
        }

        //public override Task StartAsync(CancellationToken cancellationToken)
        //{

        //}

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Task stopTask = null;
            if (_app != null)
            {
                var _config = (new ConfigurationBuilder())
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                if (!_app.StopAsync().Wait(Convert.ToInt32(_config.GetSection("Timeout").Value)))
                {
                    _app.CancelAsync().Wait();
                    _logger.LogInformation("Cancelled");
                }
                else
                {
                    _logger.LogInformation("Stopped");
                }

                _app.Dispose();
                _app = null;
            }
            return Task.CompletedTask;
        }

    }
}
