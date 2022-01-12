using Blogs.BL.Abstractions;
using Blogs.Persistence.Models;
using System;

namespace Blogs.BL.DataItemHandlers
{
    public class DataItemHandler<DTOEntity> : IDataItemHandler<DTOEntity>
    {
        private bool isDisposed = false;

        protected IDTOEntityParser<DTOEntity> DTOParser;
        protected IFetchOrInsertUnitOfWork<User> userUoW;
        protected IFetchOrInsertUnitOfWork<Blog> blogUoW;
        protected IAddEntityUoW<Comment> commentUoW;

        public DataItemHandler(
            IDTOEntityParser<DTOEntity> parser,
            IFetchOrInsertUnitOfWork<User> userUoW,
            IFetchOrInsertUnitOfWork<Blog> blogUoW,
            IAddEntityUoW<Comment> commentUoW)
        {
            this.DTOParser = parser;
            this.userUoW = userUoW;
            this.blogUoW = blogUoW;
            this.commentUoW = commentUoW;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;

            if (isDisposing)
            {

                if (userUoW != null)
                {
                    userUoW.Dispose();
                    userUoW = null;
                }

                if (blogUoW != null)
                {
                    blogUoW.Dispose();
                    blogUoW = null;
                }
                if (commentUoW != null)
                {
                    commentUoW.Dispose();
                    commentUoW = null;
                }
                DTOParser = null;
            }
            isDisposed = true;

        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DataItemHandler()
        {
            Dispose(false);
        }

        public void SaveItem(DTOEntity item)
        {
            User user = null;
            Blog blog = null;

            DTOParser.Parse(item);

            user = DTOParser.User = userUoW.PerformAction(
                x => x.FirstName == DTOParser.User.FirstName
                && x.LastName == DTOParser.User.LastName,
                new User()
                {
                    FirstName = DTOParser.User.FirstName,
                    LastName = DTOParser.User.LastName
                });
            blog = blogUoW.PerformAction(x => x.Name == DTOParser.Blog.Name,
                new Blog() { Name = DTOParser.Blog.Name, User = user });
            commentUoW.PerformAction(
                new Comment()
                {
                    Text = DTOParser.Comment.Text,
                    User = user,
                    Blog = blog,
                    Session = DTOParser.Comment.Session
                });
        }
    }
}
