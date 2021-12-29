using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarPersistanceProviderFactory 
        : IFactory<IPrincipalPersistanceProvider<AvatarViewModel>>
    {
        public IPrincipalPersistanceProvider<AvatarViewModel> CreateInstance()
        {
            return new AvatarPersistanceProvider();
        }
    }
}