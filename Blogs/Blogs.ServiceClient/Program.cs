using Blogs.BL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.ServiceClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureLogging((loggerFactory) =>
                {
                   loggerFactory.AddEventLog(new EventLogSettings()
                   {
                       LogName = "Blog Log",
                       SourceName = "BlogService",
                       Filter = (message, level) => true 
                   });
                    //.ClearProviders()
                    //.AddConfiguration(context.Configuration.GetSection("Logging"))
                    //.AddEventLog(new EventLogSettings()
                    //{
                    //    SourceName = "Blog Service",
                    //    LogName = "BlogLog"
                    //})
                    ;
                    
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<AppOptions>(hostContext.Configuration.GetSection("AppOptions"));
                });
    }
}
