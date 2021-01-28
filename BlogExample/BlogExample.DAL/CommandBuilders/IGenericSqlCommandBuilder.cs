using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.DAL.CommandBuilders
{
    public interface IGenericSqlCommandBuilder<TEntity> where TEntity : class
    {
        string CommandText { get; }
        SqlParameter[] GetParameters(TEntity entity);
    }
}
