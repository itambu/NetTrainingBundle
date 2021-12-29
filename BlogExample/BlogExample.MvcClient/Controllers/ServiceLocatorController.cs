using BlogExample.DAL.Contexts;
using BlogExample.MvcClient.Integration;
using ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class ServiceLocatorController : Controller
    {
        private IServiceLocator locator;
        protected IServiceLocator Locator => locator;

        public IDbContextFactory ContextFactory => contextFactory;

        private IDbContextFactory contextFactory;
        public ServiceLocatorController()
        {
            locator = ServiceLocatorHelper.Locator;
            contextFactory = Locator.Get<IDbContextFactory>();
        }
    }
}