using BlogExample.MvcClient.Integration;
using BlogExample.MvcClient.PermissionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BlogExample.MvcClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServiceLocatorHelper.RegisterConfig();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperHelper.MapperConfig();
            BlogDatabaseConfig.CreateIfNotExist();
        }
    }
}
