using Blogs.DAL.Abstractions;
using System.Data.Common;
using System.Data.SqlClient;

namespace Blogs.BL.ConnectionFactories
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionStr;
        public DbConnection CreateInstance(bool openOnCreate = false)
        {
            var temp = new SqlConnection(_connectionStr);
            if (openOnCreate) temp.Open();
            return temp;
        }

        public SqlConnectionFactory(string connectionString)
        {
            _connectionStr = connectionString;
        }
    }
}
