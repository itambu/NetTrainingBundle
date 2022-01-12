using System.Data.Common;
using System.Data.Entity;

namespace Blogs.DAL.Abstractions
{
    public interface IBlogContextFactory
    {
        DbContext CreateInstance(DbConnection connection = null, bool contextOwnConnection = true);
    }
}
