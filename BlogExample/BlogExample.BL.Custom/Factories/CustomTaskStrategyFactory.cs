using BlogExample.BL.Strategies;
using BlogExample.BL.Strategies.Factories;
using BlogExample.BL.Transactions;
using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Factories
{
    public class CustomTaskStrategyFactory : ILogicTaskStrategyFactory<CSVDTO, CustomLogicTaskContext>
    {
        readonly IDbContextFactory ContextFactory;
        readonly IRepositoryFactory RepoFactory;
        readonly ITransactionScopeFactory ScopeFactory;
        
        public LogicTaskStrategyEventHandlerContainer<CustomLogicTaskContext, CSVDTO> ActionContainer { get; private set; }

        public CustomTaskStrategyFactory(
            IDbContextFactory contextFactory,
            IRepositoryFactory repoFactory,
            ITransactionScopeFactory scopeFactory
            )
        {
            ContextFactory = contextFactory;
            RepoFactory = repoFactory;
            ScopeFactory = scopeFactory;
            ActionContainer = new LogicTaskStrategyEventHandlerContainer<CustomLogicTaskContext, CSVDTO>();
        }


        public IGenericLogicTaskStrategy<CSVDTO, CustomLogicTaskContext> CreateInstance(CustomLogicTaskContext taskContext)
        {
            var s = new TransactDataTaskStrategy(
                ContextFactory, 
                RepoFactory, ScopeFactory);
            s.TaskCompleted += ActionContainer.OnCompleted;
            s.TaskCancelled += ActionContainer.OnCancelled;
            s.TaskFaulted += ActionContainer.OnFaulted;
            s.TaskStarting += ActionContainer.OnStarting;
            s.DataItemHandler += ActionContainer.OnDataItemHandler;
            return s;
        }
    }
}
