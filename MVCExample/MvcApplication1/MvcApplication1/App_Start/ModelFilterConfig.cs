using MvcApplication1.Infrastucture;
using MvcApplication1.Models;
using MvcApplication1.Models.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.App_Start
{
    public static class ModelFilterConfig
    {
        public static ModelFilterFactory Factory { get; } = new ModelFilterFactory();

        public static void RegisterModelFilter()
        {
            Factory.Register<Cat, CatFilter>();
        }

        public static void RegisterStorageRoutes()
        {
            StorageRouter.Register(
                typeof(CatFilter), 
                (item, c) => c.Session[typeof(CatFilter).FullName],
                (item, c) => { c.Session[typeof(CatFilter).FullName] = item; }
            );

            StorageRouter.Register(
                typeof(PagedList<Cat>),
                (item, c) => c.ViewData[typeof(PagedList<Cat>).FullName],
                (item, c) => { c.ViewData[typeof(PagedList<Cat>).FullName] = item; }
            );
        }
    }
}