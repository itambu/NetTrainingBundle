using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogExample.MvcClient.Authorization;
using BlogExample.WebClientBL.Models;
using PersistanceService;
using ServiceLocator;
using BlogExample.MvcClient.Integration.SessionPersistance;
using BlogExample.MvcClient.Models;
using BlogExample.DAL.Contexts;
using BlogExample.MvcClient.FilterModels;
using BlogExample.MvcClient.Integration.SessionPersistance.Avatar;
using BlogExample.MvcClient.PermissionService;
using AutoMapper;

namespace BlogExample.MvcClient.Integration
{
    public static class ServiceLocatorHelper
    {
        public static IServiceLocator Locator { get; private set; }

        static ServiceLocatorHelper()
        {
            Locator = new ServiceLocator.ServiceLocator(new Dictionary<Type, object>());
        }

        public static void RegisterConfig()
        {
            Locator
                .Register<IDbContextFactory>(new DbContextFactory())
                .Register<MapperConfiguration>(MapperHelper.MapperConfig())
                .Register<IMapper>( new Mapper(Locator.Get<MapperConfiguration>()))
                .Register<PermissionConfiguration>(new PermissionConfiguration()
                    .Register<Blog, User>((blog, user, principal) => blog.User.Id == user.Id, OperationPermission.Update)
                    .Register<Blog, User>((blog, user, principal) => principal.IsInRole("Admins"), OperationPermission.Update))
                .Register<IPermissionProvider>(new PermissionProvider(Locator.Get<PermissionConfiguration>()))
                .Register<IParametrizedFactory<IPersistanceHandler<AvatarViewModel>, HttpSessionStateBase>>(
                    new AvatarViewModelHandlerFactory())
                .Register<IFactory<IPrincipalPersistanceProvider<AvatarViewModel>>>(
                    new AvatarPersistanceProviderFactory())
                .Register<IParametrizedFactory<IPersistanceManager<AvatarViewModel>, HttpContextBase>>(
                    new AvatarPersistanceManagerFactory(
                        Locator.Get<IParametrizedFactory<IPersistanceHandler<AvatarViewModel>, HttpSessionStateBase>>(),
                        Locator.Get<IFactory<IPrincipalPersistanceProvider<AvatarViewModel>>>()
                        )
                    )
                //.Register<IParametrizedFactory<IPersistanceHandler<BlogFilter>, HttpSessionStateBase>>(
                //    new BlogFilterHandlerFactory())
                .Register<IParametrizedFactory<IPersistanceManager<BlogFilter>, HttpContextBase>>(new BlogPersistanceManagerFactory(
                    Locator.Get<IParametrizedFactory<IPersistanceManager<AvatarViewModel>, HttpContextBase>>()
                    ));
        }
    }
}