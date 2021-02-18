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

namespace BlogExample.MvcClient
{
    public static class Extensions
    {
        public static void ResetUserName(this Controller controller)
        {
            controller.Session["UserName"] = null;
        }

        public static User GetAuthorizedUser(this Controller controller, DbContext context)
        {
            return (controller.User.Identity.IsAuthenticated) ?
                (new GenericRepository<User>(context)).SingleOrDefault(x => x.EMail == controller.User.Identity.Name) : null;
        }

        public static BlogFilter LoadBlogFilterFromSessionOrDefault(this Controller controller)
        {
            BlogFilter filter = (BlogFilter)controller.Session[typeof(BlogFilter).FullName];
            if (filter == null)
            {
                try
                {
                    using (var context = new BlogContext())
                    {
                        DateTime temp = DateTime.Now;
                        filter = new BlogFilter()
                        {
                            AuthorizedUserId = controller.GetAuthorizedUser(context).Id,
                            From = temp,
                            To = temp
                        };
                    }
                    controller.SaveBlogFilterInSession(filter);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Could not manage blog filter object", e.InnerException);
                }
            };
            return filter;
        }

        public static void SaveBlogFilterInSession(this Controller controller, BlogFilter filter)
        {
            controller.Session[filter.GetType().FullName] = filter;
        }
    }
}