using Blogs.Persistence.Contexts;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Text;

namespace Blogs.DAL.Abstractions
{
    public interface IBlogContextFactory
    {
        DbContext CreateInstance(DbConnection connection, bool contextOwnConnection = true);
    }
}
