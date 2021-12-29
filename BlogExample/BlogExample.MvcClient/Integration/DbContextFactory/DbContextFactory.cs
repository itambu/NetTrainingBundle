using BlogExample.DAL.Contexts;
using BlogExample.WebClientBL.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace BlogExample.MvcClient.Integration
{
    public class DbContextFactory : IDbContextFactory
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