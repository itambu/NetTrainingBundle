using Blogs.BL.Abstractions;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.DataItemHandlers;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Blogs.BL.DataSourceHandlers
{
    public class DataSourceHandleBuilder<DTOEntity> : IDataSourceHandleBuilder<DTOEntity>
    {
        public virtual IDataSourceHandlerFactory<DTOEntity> HandlerFactory { protected get; set; }
        public virtual IConnectionFactory ConnectionFactory { protected get; set; }
        public virtual IBlogContextFactory ContextFactory { protected get; set; }
        public virtual IRepositoryFactory ReposFactory { protected get; set; }
        public virtual IDTOParserFactory<DTOEntity> ParserFactory { protected get; set; }
        public virtual CancellationTokenSource CancelTokenSource { protected get; set; }
        public IDataSourceHandler Build(IBlogDataSource<DTOEntity> source)
        {
            var connection = ConnectionFactory.CreateInstance();
            DbContext context = ContextFactory.CreateInstance(connection, true);

            IGenericRepository<User> userRepo = ReposFactory.CreateInstance<User>(context);
            IGenericRepository<Blog> blogRepo = ReposFactory.CreateInstance<Blog>(context);
            IGenericRepository<Comment> commentRepo = ReposFactory.CreateInstance<Comment>(context);
            
            IDataItemHandler<DTOEntity> itemHandler = new DataItemHandler<DTOEntity>(
                 ParserFactory.CreateInstance(),
                 new FetchOrInsertUoW<User>(userRepo),
                 new FetchOrInsertUoW<Blog>(blogRepo),
                 new AddEntityUoW<Comment>(commentRepo)
                 );

            return HandlerFactory.CreateInstance(source, itemHandler, CancelTokenSource.Token);
        }
    }
}
