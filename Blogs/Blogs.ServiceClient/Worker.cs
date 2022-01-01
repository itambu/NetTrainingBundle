using Blogs.BL.Abstractions;
using Blogs.BL.StartApp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            var temp = base.StartAsync(stoppingToken);

            _app = new ConfiguredApp();
            _startAppTask = _app.StartAsync();
            return Task.WhenAll(temp, _startAppTask);
        }

        //public override Task StartAsync(CancellationToken cancellationToken)
        //{

        //}

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_app!=null)
            {
                _app.StopAsync().Wait();
                _app.Dispose();
                _app = null;
            }
            return base.StopAsync(cancellationToken);
        }

    }
}
