using System;
using Blogs.BL.Abstractions;
using Blogs.BL.StartApp;
using Blogs.Demo.ConsoleApp;

namespace Blogs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IAsyncApp app = new ConsoleClientApp())
            {
                Console.Write("Starting...");
                app.StartAsync().Wait();
                Console.WriteLine("listening");
                Console.ReadKey(true);
                app.StopAsync().Wait();
                Console.WriteLine("Stopped");
            }
        }
    }
}
