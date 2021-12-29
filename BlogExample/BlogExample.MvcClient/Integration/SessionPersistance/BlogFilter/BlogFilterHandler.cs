using BlogExample.MvcClient.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class BlogFilterHandler : SessionPersistanceHandler<BlogFilter>
    {
        public BlogFilterHandler(HttpSessionStateBase session) 
            : base(session, "blogFilterSessionKey")
        {
        }
    }
}