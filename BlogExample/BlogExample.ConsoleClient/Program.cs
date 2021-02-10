using BlogExample.BL.Custom.DataContext;
using BlogExample.BL.Custom.DataSourceProviders;
using System;
using System.Data.Entity;

namespace BlogExample.ConsoleClient
{
    class Program
    {
        static void InitilizeDependencies()
        {
        }
        static void Main(string[] args)
        {
            using (DbContext context = new BlogContext())
            {
                context.Database.CreateIfNotExists();
            }

            var builder = new ConloseMessageBuilder();
            var handler = builder.Build();
            handler.DataSourceProvider = new SAXFileProvider();

            try
            {
                handler.Start();
                handler.Wait();
                handler.Stop();
            }
            finally
            {
                handler.Dispose();
            }
        }
    }
}
