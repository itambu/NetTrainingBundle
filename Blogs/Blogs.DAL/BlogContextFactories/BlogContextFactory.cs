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
        private IConnectionFactory _connectionFactory;
        protected bool OpenOnCreate { get; set; } = false;

        public DbContext CreateInstance(DbConnection connection=null, bool contextOwnConnection = true)
        {
            var temp = (connection == null) ? _connectionFactory.CreateInstance(OpenOnCreate)
                : connection;
            if (temp == null) throw new InvalidOperationException("No connection object can be substituted");
            return new BlogDbContext(temp, contextOwnConnection);
        }

        public BlogContextFactory(IConnectionFactory connectionFactory=null)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
