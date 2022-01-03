using Blogs.BL.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.ServiceClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, builder) =>
            {
                //builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                builder.AddJsonFile("appsettings.json", true, true);
                builder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
            })
            .ConfigureHostConfiguration(hostBuilder =>
            {
                //hostBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            })
            .UseWindowsService()
            .ConfigureLogging((loggerFactory) =>
            {
                loggerFactory.AddEventLog(new EventLogSettings()
                {
                    LogName = "Blog Log",
                    SourceName = "BlogService",
                    Filter = (message, level) => true
                });
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.Configure<AppOptions>(hostContext.Configuration.GetSection("AppOptions"));
            });
        }

    }

}
