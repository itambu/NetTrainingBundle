using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.Integration;
using BlogExample.MvcClient.Integration.SessionPersistance;
using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Contexts;
using BlogExample.WebClientBL.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public static class SessionSupport
    {
        public static ViewModel GetViewModel<ViewModel>(
            this HttpContextBase httpContext)
        {
            return ServiceLocatorHelper.Locator.Get<IParametrizedFactory<IPersistanceManager<ViewModel>, HttpContextBase>>()
                .CreateInstance(httpContext).Get();
        }
    }
}