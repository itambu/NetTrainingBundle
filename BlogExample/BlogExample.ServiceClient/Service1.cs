using BlogExample.BL.CSVParsing;
using BlogExample.BL.Custom;
using BlogExample.BL.Custom.Builders;
using BlogExample.BL.Custom.DataContext;
using BlogExample.BL.Custom.DataSourceProviders;
using BlogExample.BL.FileProviders;
using BlogExample.BL.Strategies;
using BlogExample.BL.TaskSchedulers;
using BlogExample.BL.Transactions;
using BlogExample.DAL.Repositories.Factories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogExample.ServiceClient
{
    public partial class BlogService : ServiceBase
    {
        GenericProcessStrategy<CSVDTO, CustomLogicTaskContext> handler;

        public BlogService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                using (DbContext context = new BlogContext())
                {
                    context.Database.CreateIfNotExists();
                }
        //        Thread.Sleep(15000);
                var builder = new BackupFeatureBuilder();
                handler = builder.Build();
                handler.DataSourceProvider = new WatcherFileProvider(new FileSystemWatcher());
                handler.Start();
            }
            catch(Exception e)
            {
                // log exception
                handler.Dispose();
            }
            ////BlogContext context = new BlogContext();
            ////context.Database.Delete();
            ////context.Database.Create();
            //Thread.Sleep(15000);
            //var stream = File.AppendText(@"d:\temp\log.txt");
            //stream.WriteLine("kuku");
            //stream.Flush();

            //var saxProvider = new SAXFileProvider();
            //var scheduler = new ParsingTaskScheduler();
            //cancellationTokenSource = new System.Threading.CancellationTokenSource();
            //var taskFactory = new TaskFactory(
            //        cancellationTokenSource.Token,
            //        TaskCreationOptions.None,
            //        TaskContinuationOptions.HideScheduler, scheduler);
            //var taskManager = new TaskManager(taskFactory, new ConcurrentQueue<Task>());
            //var contextFactory = new BlogContextFactory();
            //var repoFactory = new TransactionalRepositotyFactory(contextFactory);
            //var scopeFactory = new TransactionScopeFactory();

            //manager = new TransactionProcessManager(
            //        saxProvider,
            //        taskManager,
            //        contextFactory,
            //        repoFactory,
            //        scopeFactory
            //    );
            //stream.WriteLine("end init");
            //stream.Flush();
            ////manager.TaskCompleted += (sender, x) => { Console.WriteLine("completed"); };
            ////manager.TaskCancelled += (sender, x) => { Console.WriteLine("cancelled"); };
            ////manager.TaskFaulted += (sender, x) => { Console.WriteLine("faulted"); };
            //try
            //{
            //    stream.WriteLine("starting");
            //    manager.Start();
            //    stream.WriteLine("end starting");
            //    manager.Wait();
            //    stream.WriteLine("end waiting");
            //    stream.Flush();

            //    //manager.DataSourceProvider =
            //    //    new WatcherFileProvider(new System.IO.FileSystemWatcher());
            //    //manager.Start();
            //}
            //catch (Exception e)
            //{
            //    stream.WriteLine(e.Message);
            //    stream.Flush();
            //}
        }

        protected override void OnStop()
        {
            try
            {
                handler.Stop();
            }
            catch (Exception e)
            {
                // log exception
                handler?.Dispose();
            }
        }
    }
}
