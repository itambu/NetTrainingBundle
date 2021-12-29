using BlogExample.WebClientBL.Contexts;
using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.FilterModels;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using BlogExample.MvcClient.Models;
using BlogExample.MvcClient.Authorization;

namespace BlogExample.MvcClient
{
    public static class Extensions
    {
        //public static BlogFilter LoadBlogFilterFromSessionOrDefault(this Controller controller)
        //{
        //    BlogFilter filter = (BlogFilter)controller.Session[typeof(BlogFilter).FullName];
        //    if (filter == null)
        //    {
        //        try
        //        {
        //            DateTime temp = DateTime.Now;
        //            filter = new BlogFilter()
        //            {
        //                AuthorizedUserId = HttpContext.GetViewModel()(controller.User).Id,
        //                From = temp,
        //                To = temp
        //            };
        //            controller.SaveBlogFilterInSession(filter);
        //        }
        //        catch (Exception e)
        //        {
        //            throw new InvalidOperationException("Could not manage blog filter object", e.InnerException);
        //        }
        //    };
        //    return filter;
        //}

        //public static void SaveBlogFilterInSession(this Controller controller, BlogFilter filter)
        //{
        //    controller.Session[filter.GetType().FullName] = filter;
        //}
    }
}