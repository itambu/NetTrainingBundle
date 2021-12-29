using BlogExample.MvcClient.FilterModels;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class BlogFilterHandlerFactory : IParametrizedFactory<IPersistanceHandler<BlogFilter>, HttpSessionStateBase>
    {
        public IPersistanceHandler<BlogFilter> CreateInstance(HttpSessionStateBase session)
        {
            return new BlogFilterHandler(session);
        }
    }
}