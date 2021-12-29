using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarPersistanceManagerFactory : 
        IParametrizedFactory<IPersistanceManager<AvatarViewModel>, HttpContextBase>
    {
        IParametrizedFactory<
            IPersistanceHandler<AvatarViewModel>, 
            HttpSessionStateBase> handlerFactory;
        IFactory<IPrincipalPersistanceProvider<AvatarViewModel>> providerFactory;
        public IPersistanceManager<AvatarViewModel> CreateInstance(HttpContextBase context)
        {
            return new PersistanceManager<AvatarViewModel>(
                handlerFactory.CreateInstance(context.Session), 
                    providerFactory.CreateInstance(), 
                    context.User);
        }

        public AvatarPersistanceManagerFactory(
            IParametrizedFactory<IPersistanceHandler<AvatarViewModel>, HttpSessionStateBase> handlerFactory,
            IFactory<IPrincipalPersistanceProvider<AvatarViewModel>> providerFactory
            )
        {
            this.providerFactory = providerFactory;
            this.handlerFactory = handlerFactory;
        }
    }
}