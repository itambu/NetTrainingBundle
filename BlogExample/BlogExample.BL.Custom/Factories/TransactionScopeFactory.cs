using BlogExample.BL.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlogExample.BL.Custom.Factories
{
    public class TransactionScopeFactory : ITransactionScopeFactory
    {
        public TransactionScope Create(TransactionScopeOption? scopeOption = null, IsolationLevel? isolation = null)
        {
            return new TransactionScope(
               scopeOption ?? TransactionScopeOption.RequiresNew,
               new TransactionOptions() { IsolationLevel = isolation ?? IsolationLevel.ReadCommitted },
               TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
