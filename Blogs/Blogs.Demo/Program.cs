using Blogs.Persistence.Contexts;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Blogs.Persistence.Models;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.Abstractions;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.DAL.Abstractions;
using Blogs.DAL.Repositories;

using Blogs.BL.DataSourceHandlers;
using Blogs.BL.BlogDataSources;
using ChinhDo.Transactions;
using System.Transactions;
using System.Data.SqlClient;
using Blogs.BL.ConnectionFactories;
using System.Data;
using System.Data.Common;
using Blogs.DAL.BlogContextFactories;
using System.Data.Entity;
using Blogs.BL.DTOEntityParsers;
using Blogs.DAL.RepositotyFactories;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.ProcessManagers;
using Blogs.BL.FolderDataSourceProviders;
using System.Threading;
using System.Threading.Tasks;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.ParallelismHandlers;
using System.Collections.Concurrent;
using Blogs.BL.StartApp;

namespace Blogs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.Start().Wait();
            Console.WriteLine("Started and Listening");
            Console.ReadKey();
            app.Stop().Wait();
            Console.WriteLine("Stopped");
            Console.ReadKey();
            app.Dispose();
        }
    }
}
