﻿using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<Blog> items = null;
            using (var repo = new DAL.BlogRepository())
            {
                items = repo
                .GetAll()
                .Select(x => new Blog() { Id = x.Id, Description = x.Description, PublishDate = x.PublishDate })
                .ToList();
            }
            return View(items);
        }


        public ActionResult Details(int id)
        {
            var item = new DAL.BlogRepository()
                .GetAll().Where(x=>x.Id == id)
                .Select(x => new Blog() { Id = x.Id, Description = x.Description, PublishDate = x.PublishDate })
                .FirstOrDefault();
            return PartialView(item);
        }

        [HttpGet]
        [Authorize(Roles ="Admins")]
        public ActionResult Edit(int id)
        {
            var item = new DAL.BlogRepository()
                .GetAll().Where(x=>x.Id == id)
                .Select(x => new Blog() { Id = x.Id, Description = x.Description, PublishDate = x.PublishDate })
                .FirstOrDefault();
            return PartialView("Edit", item);
        }


        [HttpPost]
        public ActionResult Edit(Blog model)
        {
            ModelState.Clear();
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                
                var item = new DAL.BlogRepository()
                    .GetAll().Where(x => x.Id == model.Id).FirstOrDefault();
                item.Description = model.Description;
                item.PublishDate = model.PublishDate;

                if (!Request.IsAjaxRequest())
                {
                    ModelState.AddModelError("Description", "Wrong");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("Description", "Wrong");
                    ViewBag.Message = "Wrong";
                    return View(model);

                }
            }
            else
            {
                return View("Error");
            }
        }

        
        public ActionResult BlogList()
        {
            var item = new DAL.BlogRepository()
                .GetAll()
                .Select(x => new Blog() { Id = x.Id, Description = x.Description, PublishDate = x.PublishDate });
            return PartialView("PartialBlogList", item);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetChartData()
        {
            Random rnd = new Random();
            var item = new DAL.BlogRepository()
                .GetAll()
                .Select(x => new object[] { x.Description, rnd.Next(10)} ).ToArray();

            //return Json(item, "", Encoding.UTF8 ,JsonRequestBehavior.AllowGet ); 

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public BlogController()
        {

        }
    }
}
