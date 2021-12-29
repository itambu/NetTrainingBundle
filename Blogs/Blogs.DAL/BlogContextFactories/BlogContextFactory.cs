using Blogs.DAL.Abstractions;
using Blogs.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DAL.BlogContextFactories
{
    public class BlogContextFactory : IBlogContextFactory
    {
        public DbContext CreateInstance(DbConnection connection, bool contextOwnConnection = true)
        {
            return new BlogDbContext(connection, contextOwnConnection);
        }
    }
}
