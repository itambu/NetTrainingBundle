using System;
using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.BL.StartApp;
using Blogs.Demo.ConsoleApp;
using Microsoft.Extensions.Configuration;

namespace Blogs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //IConfiguration _config = (new ConfigurationBuilder())
            //   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json").Build();

            //using (IAsyncApp app = new ConsoleClientApp(_config.GetSection("AppOptions").Get<AppOptions>()))
            //{
            //    Console.Write("Starting...");
            //    app.StartAsync().Wait();
            //    Console.WriteLine("listening");
            //    Console.ReadKey(true);
            //    app.StopAsync().Wait();
            //    Console.WriteLine("Stopped");
            //}
        }
    }
}
