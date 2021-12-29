using Blogs.BL.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.ConnectionFactories
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IConfigurationRoot _config;
        public DbConnection CreateInstance(bool openOnCreate = false)
        {
            var temp = new SqlConnection(_config.GetConnectionString("blog_db_test"));
            if (openOnCreate) temp.Open();
            return temp;
        }

        public SqlConnectionFactory(IConfigurationRoot config)
        {
            _config = config;
        }
    }
}
