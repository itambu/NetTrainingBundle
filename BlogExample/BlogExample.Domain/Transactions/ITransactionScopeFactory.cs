using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlogExample.BL.Transactions
{
    public interface ITransactionScopeFactory
    {
        TransactionScope Create(TransactionScopeOption? scopeOption = null, IsolationLevel? isolation = null);
    }
}
