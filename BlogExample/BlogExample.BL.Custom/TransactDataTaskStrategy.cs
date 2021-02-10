using BlogExample.BL.Absractions;
using BlogExample.BL.Custom.Operations;
using BlogExample.BL.Strategies;
using BlogExample.BL.Transactions;
using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories.Factories;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class TransactDataTaskStrategy : GenericLogicTaskStrategy<CSVDTO, CustomLogicTaskContext>
    {
        protected IDbContextFactory ContextFactory { get; set; }
        protected IRepositoryFactory RepositoryFactory { get; set; }
        protected ITransactionScopeFactory TransactionScopeFactory { get; set; }

        EventHandler<CustomLogicTaskContext> _dataItemHandler;
        public override EventHandler<CustomLogicTaskContext> DataItemHandler 
        { 
            get => _dataItemHandler; 
            set => _dataItemHandler = value; }

        public TransactDataTaskStrategy(
            IDbContextFactory contextFactory,
            IRepositoryFactory repositoryFactory,
            ITransactionScopeFactory transactionScopeFactory
            ) : base()
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
            TransactionScopeFactory = transactionScopeFactory;
            DataItemHandler += (sender, taskScope) => { OnDataItemHandler(taskScope); };
        }

        protected override void Disposing()
        {
            ContextFactory = null;
            RepositoryFactory = null;
            TransactionScopeFactory = null;
            base.Disposing();
        }

        protected void OnDataItemHandler(CustomLogicTaskContext taskContext)
        {
            DbContext context = ContextFactory.CreateInstance();
            try
            {
                context.Database.Connection.Open();
                var userRepo = RepositoryFactory.CreateInstance<User>(context);

                User user = null;
                using (var scope1 = TransactionScopeFactory.Create())
                {
                    IUnitOfWork unitOfWork =
                        new AddUserOperation(userRepo, scope1)
                        {
                            User = new User() { Nickname = taskContext.DataItem.NickName }
                        };
                    unitOfWork.Execute();
                }
                user = userRepo.SingleOrDefault(x => x.Nickname == taskContext.DataItem.NickName);

                var blogRepo = RepositoryFactory.CreateInstance<Blog>(context);
                var blog = new Blog()
                {
                    Topic = taskContext.DataItem.Topic,
                    User = user,
                    Session = taskContext.Session
                };
                blogRepo.Add(blog);
                blogRepo.Save();

                context.Database.Connection.Close();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Task failed", e);
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}
