using BlogExample.MvcClient.FilterModels;
using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class BlogPersistanceManagerFactory : IParametrizedFactory<IPersistanceManager<BlogFilter>, HttpContextBase>
    {
        IParametrizedFactory<IPersistanceManager<AvatarViewModel>, HttpContextBase> avatarManagerFactory;

        public BlogPersistanceManagerFactory(
                IParametrizedFactory<IPersistanceManager<AvatarViewModel>, HttpContextBase> avatarManagerFactory
            )
        {
            this.avatarManagerFactory = avatarManagerFactory;
        }

        public IPersistanceManager<BlogFilter> CreateInstance(HttpContextBase context)
        {
            return new BlogPersistanceManager(new BlogFilterHandler(context.Session), avatarManagerFactory.CreateInstance(context));
        }
    }
}