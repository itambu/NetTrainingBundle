using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient
{
    public static class BlogDatabaseConfig
    {
        public static void CreateIfNotExist()
        {
            (new BlogContext()).Database.CreateIfNotExists();
            ApplicationDbContext.Create().Database.CreateIfNotExists(); 
        }
    }
}