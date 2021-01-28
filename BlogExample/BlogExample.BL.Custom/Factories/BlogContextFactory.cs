using BlogExample.BL.Custom.DataContext;
using BlogExample.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Factories
{
    public class BlogContextFactory : IDbContextFactory
    {
        public DbContext CreateInstance()
        {
            return new BlogContext();
        }

        public DbContext CreateInstance(DbConnection connection, bool contextOwnsConnection)
        {
            return new BlogContext(connection, contextOwnsConnection);
        }
    }
}
