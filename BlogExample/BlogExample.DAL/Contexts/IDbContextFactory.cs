using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.DAL.Contexts
{
    public interface IDbContextFactory
    {
        DbContext CreateInstance();
        DbContext CreateInstance(DbConnection connection, bool contextOwnsConnection);
    }
}
