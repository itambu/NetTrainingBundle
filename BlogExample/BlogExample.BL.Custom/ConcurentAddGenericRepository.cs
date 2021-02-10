using BlogExample.DAL.CommandBuilders;
using BlogExample.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class ConcurentAddGenericRepositoty<TEntity> : GenericRepository<TEntity>, IGenericRepository<TEntity> where TEntity : class
    {
        readonly IGenericSqlCommandBuilder<TEntity> _commandBuilder = null;

        public ConcurentAddGenericRepositoty(DbContext context)
            : base(context)
        {
            _commandBuilder = new ConcurrentAddWhenNotExistCommandBuilder<TEntity>(context);
        }

        public new void Add(TEntity entity)
        {
            Context.Database.ExecuteSqlCommand(
                                            _commandBuilder.CommandText,
                                            _commandBuilder.GetParameters(entity)
                                        );
            Attach(entity);
            //Context.Entry(entity).Reload();
        }
    }
}
