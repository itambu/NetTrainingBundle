using BlogExample.MvcClient.FilterModels;
using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class BlogPersistanceManager : IPersistanceManager<BlogFilter>
    {
        IPersistanceHandler<BlogFilter> handler;
        IPersistanceManager<AvatarViewModel> avatarManager;

        public BlogPersistanceManager(
            IPersistanceHandler<BlogFilter> handler,
            IPersistanceManager<AvatarViewModel> avatarManager
            )
        {
            this.handler = handler;
            this.avatarManager = avatarManager;
        }
        public void Clear() => handler.Clear();

        public BlogFilter Get()
        {
            var temp = handler.Get();
            return temp != null ? temp : new BlogFilter()
            {
                AuthorizedUserId = avatarManager.Get().Id,
                From = DateTime.Now,
                To = DateTime.Now
            };
        }

        public void Save(BlogFilter model) => handler.Save(model);
    }
}