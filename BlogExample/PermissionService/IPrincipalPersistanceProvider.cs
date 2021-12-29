using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{

    public interface IPrincipalPersistanceProvider<T>
    {
        T Handle(IPrincipal principal, IPersistanceHandler<T> persistanceHandler);    
    }
}
