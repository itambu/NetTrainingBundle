using BlogExample.BL.Absractions;
using BlogExample.DAL.Repositories;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlogExample.BL.Custom.Operations
{
    public class AddUserOperation : IUnitOfWork
    {
        public User User { get; set; }

        protected IGenericRepository<User> UserRepo;

        protected TransactionScope Scope { get; set; }

        public AddUserOperation(
            IGenericRepository<User> userRepo,
            TransactionScope scope
            )
        {
            UserRepo = userRepo;
            Scope = scope;
        }

        public void Commit()
        {
            Scope.Complete();
        }

        public void Execute()
        {
            try
            {
                if (UserRepo.SingleOrDefault(x => x.Nickname == User.Nickname) == null)
                {
                    UserRepo.Add(User);
                    UserRepo.Save();
                }
                Scope.Complete();
            }
            catch(NullReferenceException e)
            {
                Rollback();
                throw e;
            }
            catch (TransactionException e)
            {
                Rollback();
                throw new InvalidOperationException("Adding user failed", e );
            }
        }
        public void Rollback()
        {
            // automatic rollback if Scope.Complete() was not called
        }
    }
}
