using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.DataItemHandlers
{
    public class DataItemHandlerFactory : IDataItemHandlerFactory<BlogDataSourceDTO>
    {
        protected IRepositoryFactory _repositoryFactory;
        protected IBlogContextFactory _blogContextFactory;
        protected EntityConcurrencyHandler _entityConcurrencyHandler;
        IDTOParserFactory<BlogDataSourceDTO> _parserFactory;

        public IDataItemHandler<BlogDataSourceDTO> CreateInstance()
        {
            DbContext context = _blogContextFactory.CreateInstance();

            IGenericRepository<User> userRepo = _repositoryFactory.CreateInstance<User>(context);
            IGenericRepository<Blog> blogRepo = _repositoryFactory.CreateInstance<Blog>(context);
            IGenericRepository<Comment> commentRepo = _repositoryFactory.CreateInstance<Comment>(context);

            return new DataItemHandler<BlogDataSourceDTO>(_parserFactory.CreateInstance(),
                 new FetchOrInsertUoW<User>(userRepo, _entityConcurrencyHandler),
                 new FetchOrInsertUoW<Blog>(blogRepo, _entityConcurrencyHandler),
                 new AddEntityUoW<Comment>(commentRepo)
                );
        }

        public DataItemHandlerFactory(
            IBlogContextFactory blogContextFactory,
            IRepositoryFactory repositoryFactory,
            IDTOParserFactory<BlogDataSourceDTO> parserFactory,
            EntityConcurrencyHandler entityConcurrencyHandler
            )
        {
            _blogContextFactory = blogContextFactory;
            _repositoryFactory = repositoryFactory;
            _parserFactory = parserFactory;
            _entityConcurrencyHandler = entityConcurrencyHandler;
        }
    }
}
