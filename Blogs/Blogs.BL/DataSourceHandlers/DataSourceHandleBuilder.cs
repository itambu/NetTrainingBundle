using Blogs.BL.Abstractions;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System.Data.Entity;
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
        public virtual CancellationToken CancelToken { protected get; set; }
        public virtual EntityConcurrencyHandler EntityConcurrencyHandler { protected get; set; }

        public IDataSourceHandler Build(IBlogDataSource<DTOEntity> source)
        {
            var connection = ConnectionFactory.CreateInstance();
            DbContext context = ContextFactory.CreateInstance(connection);

            IGenericRepository<User> userRepo = ReposFactory.CreateInstance<User>(context);
            IGenericRepository<Blog> blogRepo = ReposFactory.CreateInstance<Blog>(context);
            IGenericRepository<Comment> commentRepo = ReposFactory.CreateInstance<Comment>(context);
            
            IDataItemHandler<DTOEntity> itemHandler = new DataItemHandler<DTOEntity>(
                 ParserFactory.CreateInstance(),
                 new FetchOrInsertUoW<User>(userRepo, EntityConcurrencyHandler),
                 new FetchOrInsertUoW<Blog>(blogRepo, EntityConcurrencyHandler),
                 new AddEntityUoW<Comment>(commentRepo)
                 );

            return HandlerFactory.CreateInstance(source, itemHandler, CancelToken);
        }
    }
}
