using BlogExample.BL.Custom.DataSourceProviders;
using BlogExample.BL.Custom.Factories;
using BlogExample.BL.FileProviders;
using BlogExample.BL.LogicTaskContexts.Factories;
using BlogExample.BL.LogicTaskHandlers;
using BlogExample.BL.Strategies;
using BlogExample.BL.Strategies.Factories;
using BlogExample.BL.TaskSchedulers;
using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories.Factories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Builders
{
    public class CustomProcessStrategyBuilder
    {
        protected ILogicTaskHandler LogicTaskHandler { get; set; }
        protected IDbContextFactory DbContextFactory { get; set; }
        protected IRepositoryFactory RepositoryFactory { get; set; }
        protected ILogicTaskContextFactory<CustomLogicTaskContext,CSVDTO> TaskContextFactory { get; set; }
        protected ILogicTaskStrategyFactory<CSVDTO, CustomLogicTaskContext> TaskStrategyFactory { get; set; }
        protected GenericProcessStrategy<CSVDTO, CustomLogicTaskContext> ProcessStrategy { get; set; }

        protected virtual void Building()
        {
            BuildDbContextFactory();
            BuildTaskHandler();
            BuildRepositoryFactory();
            BuildTaskContextFactory();
            BuildTaskStrategyFactory();
            BuildProcessStrategy();
        }

        public GenericProcessStrategy<CSVDTO, CustomLogicTaskContext> Build()
        {
            Building();
            return ProcessStrategy;
        }

        protected virtual void BuildTaskHandler()
        {
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            var scheduler = new ParsingTaskScheduler(new ConcurrentQueue<Task>());

            var taskFactory = new TaskFactory(
                    cancellationTokenSource.Token,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.HideScheduler, scheduler);

            LogicTaskHandler = new LogicTaskHandler(taskFactory, new ConcurrentQueue<Task>());
        }

        protected virtual void BuildDbContextFactory()
        {
            DbContextFactory = new BlogContextFactory();
        }

        protected virtual void BuildRepositoryFactory()
        {
            RepositoryFactory = new TransactionalRepositotyFactory(DbContextFactory);
        }

        protected virtual void BuildTaskContextFactory()
        {
            TaskContextFactory = new CustomLogicTaskContextFactory();
        }

        protected virtual void BuildTaskStrategyFactory()
        {
            TaskStrategyFactory = new CustomTaskStrategyFactory
                (DbContextFactory, 
                RepositoryFactory,
                new TransactionScopeFactory()
                );
            TaskStrategyFactory.ActionContainer.OnDataItemHandler +=
                (sender, taskContext) => { taskContext.CancellationToken.ThrowIfCancellationRequested(); };
        }

        protected virtual void BuildProcessStrategy()
        {
            ProcessStrategy = new GenericProcessStrategy<CSVDTO, CustomLogicTaskContext>
                (LogicTaskHandler,
                TaskStrategyFactory,
                TaskContextFactory);
        }

    }
}
