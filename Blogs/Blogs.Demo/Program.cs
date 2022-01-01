using System;
using Blogs.BL.Abstractions;
using Blogs.BL.StartApp;

namespace Blogs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IAsyncApp app = new App();
            app.StartAsync().Wait();

            Console.WriteLine("Started and Listening");
            Console.ReadKey();
            app.StopAsync().Wait();
            Console.WriteLine("Stopped");
            Console.ReadKey();
            app.Dispose();
        }
    }
}
