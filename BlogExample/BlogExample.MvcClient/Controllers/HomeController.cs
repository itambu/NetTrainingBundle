﻿using AutoMapper.QueryableExtensions;
using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Contexts;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IEnumerable<BlogHomeViewModel> blogs;
            using(var context = new BlogContext())
            {
                blogs = await new GenericRepository<Blog>(context)
                    .Get()
                    .OrderByDescending(x => x.Created)
                    .Take(10)
                    .ProjectTo<BlogHomeViewModel>(MapperHelper.Config)
                    .ToListAsync();
            }
            return View(blogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}