using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarViewModelHandlerFactory: IParametrizedFactory<IPersistanceHandler<AvatarViewModel>, HttpSessionStateBase>
    {
        public IPersistanceHandler<AvatarViewModel> CreateInstance(HttpSessionStateBase session)
        {
            return new AvatarPersistanceHandler(session);
        }
    }
}