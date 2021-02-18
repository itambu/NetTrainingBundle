using AutoMapper.QueryableExtensions;
using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.Models;
using BlogExample.MvcClient.FilterModels;
using BlogExample.WebClientBL.Contexts;
using BlogExample.WebClientBL.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [HttpGet]
        [Authorize]
        public ActionResult Index(int? page)
        {
            return View(page ?? 1);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetPagedItemView(int? page)
        {
            try 
            {
                IPagedList<BlogSimpleViewModel> model;
                using (var context = new WebClientBL.Contexts.BlogContext())
                {
                    BlogFilter filter = this.LoadBlogFilterFromSessionOrDefault();
                    IGenericRepository<Blog> blogs = new GenericRepository<Blog>(context);
                    model = blogs.Get((new BlogFilterExpression(filter)).Query)
                            .ProjectTo<BlogSimpleViewModel>(MapperHelper.Config)
                            .OrderByDescending(x => x.Created)
                            .ToPagedList(page ?? 1, 5);
                }
                return PartialView("PagedList", model);
            }
            catch
            {
                return PartialView("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int id, int page)
        {
            try
            {
                BlogDetailViewModel model;
                using (var context = new BlogContext())
                {
                    // check the exactly one blog entity exists
                    var blog = (new GenericRepository<Blog>(context)).Get().Single(x => x.Id == id);

                    var comments = (new GenericRepository<Comment>(context))
                       .Get(x => x.Blog.Id == id)
                       .OrderBy(x => x.Created)
                       .ProjectTo<CommentViewModel>(MapperHelper.Config)
                       .ToPagedList(page, 10);

                    model = MapperHelper.Mapper.Map<Blog, BlogDetailViewModel>(
                        blog,
                        opt =>
                        {
                            opt.AfterMap((s, d) => d.Comments = comments);
                        });
                    ViewBag.CanBeChanged = VerifyPermittionForChangeOperation(context, blog.User);
                }
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // GET: Blog/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateBlogViewModel());
        }

        // POST: Blog/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var context = new BlogContext())
                {
                    var blogs = new GenericRepository<Blog>(context);
                    blogs.Add(
                        new Blog()
                        {
                            Topic = model.Topic,
                            Text = model.Text,
                            Created = DateTime.Now,
                            User = this.GetAuthorizedUser(context)
                        }); ;
                    blogs.Save();
                }
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        protected bool VerifyPermittionForChangeOperation(DbContext context, User owner)
        {
            var currentUser = this.GetAuthorizedUser(context);
            return (owner.Id == currentUser.Id || User.IsInRole("admin"));
        }

        // GET: Blog/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            try
            {
                EditBlogViewModel model;
                using (var context = new BlogContext())
                {
                    var blog = new GenericRepository<Blog>(context).Get().Single(x => x.Id == id);
                    if (!VerifyPermittionForChangeOperation(context, blog.User))
                    {
                        throw new InvalidOperationException("You can edit only your blog");
                    }
                    model = MapperHelper.Mapper.Map<EditBlogViewModel>(blog);
                }
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(EditBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var context = new BlogContext())
                {
                    var blogs = new GenericRepository<Blog>(context);
                    var blog = blogs.Get().Single(x => x.Id == model.Id);
                    blog.Topic = model.Topic;
                    blog.Text = model.Text;
                    blogs.Save();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
