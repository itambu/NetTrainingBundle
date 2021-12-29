using BlogExample.MvcClient.FilterModels;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class BlogFilterVerifyer : IVerifyBinding<BlogFilter, IPrincipal>
    {
        public bool HasBinding(BlogFilter filter, IPrincipal principal)
        {
            return true;
        }
    }
}