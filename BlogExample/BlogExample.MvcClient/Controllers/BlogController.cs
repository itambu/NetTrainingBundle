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
using BlogExample.MvcClient.Authorization;
using BlogExample.MvcClient.PermissionService;
using BlogExample.MvcClient.Integration.SessionPersistance;
using BlogExample.MvcClient.Integration;
using PersistanceService;
using AutoMapper;

namespace BlogExample.MvcClient.Controllers
{
    public class BlogController : ServiceLocatorController
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
                using (var context = ContextFactory.CreateInstance())
                {
                    //var user = HttpContext.GetViewModel<AvatarViewModel>();
                    //var currentUser = new GenericRepository<User>(context).SingleOrDefault(x => x.EMail == User.Identity.Name);

                    var filter = Locator.Get<IParametrizedFactory<IPersistanceManager<BlogFilter>, HttpContextBase>>()
                        .CreateInstance(HttpContext).Get();
                    IGenericRepository<Blog> blogs = new GenericRepository<Blog>(context);
                    model = blogs.Get(new BlogFilterExpression(filter).Query)
                            .ProjectTo<BlogSimpleViewModel>(Locator.Get<IMapper>().ConfigurationProvider)
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
                using (var context = ContextFactory.CreateInstance())
                {
                    // check the exactly one blog entity exists
                    var blog = (new GenericRepository<Blog>(context)).Get().Single(x => x.Id == id);

                    ValidateEntityPermission(context, blog);

                    var comments = (new GenericRepository<Comment>(context))
                       .Get(x => x.Blog.Id == id)
                       .OrderBy(x => x.Created)
                       .ProjectTo<CommentViewModel>(Locator.Get<IMapper>().ConfigurationProvider)
                       .ToPagedList(page, 10);

                    model = Locator.Get<IMapper>().Map<Blog, BlogDetailViewModel>(
                        blog,
                        opt =>
                        {
                            opt.AfterMap((s, d) => d.Comments = comments);
                        });
                }
                return View(model);
            }
            catch(PermissionOperationException)
            {
                return View("Forbidden");
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
                using (var context = ContextFactory.CreateInstance())
                {
                    var blogs = new GenericRepository<Blog>(context);
                    var users = new GenericRepository<User>(context);
                    var userId = HttpContext.GetViewModel<AvatarViewModel>().Id;
                    blogs.Add(
                        new Blog()
                        {
                            Topic = model.Topic,
                            Text = model.Text,
                            Created = DateTime.Now,
                            User = users.Get().Single(x => x.Id == userId)
                        });
                    blogs.Save();
                }
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            try
            {
                EditBlogViewModel model;
                using (var context = ContextFactory.CreateInstance())
                {
                    var blog = new GenericRepository<Blog>(context).Get().Single(x => x.Id == id);
                    ValidateEntityPermission(context, blog);
                    model = Locator.Get<IMapper>().Map<EditBlogViewModel>(blog);
                }
                return View(model);
            }
            catch(PermissionOperationException e)
            {
                return View("Forbidden", e.Message);
            }
            catch
            {
                return View("Error");
            }
        }

        protected void ValidateEntityPermission(DbContext context, Blog blog)
        {
            var userId = HttpContext.GetViewModel<AvatarViewModel>().Id;
            var user = new GenericRepository<User>(context).Get().Single(x => x.Id == userId);
            if (!Locator.Get<IPermissionProvider>().HasPermission<Blog, User>(blog,
                user, this.User, OperationPermission.Update))
            {
                ViewBag.HasUpdatePermission = false;
                throw new PermissionOperationException(user.Nickname);
            }
            else
            {
                ViewBag.HasUpdatePermission = true;
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
                using (var context = ContextFactory.CreateInstance())
                {
                    var blogs = new GenericRepository<Blog>(context);
                    var blog = blogs.Get().Single(x => x.Id == model.Id);

                    ValidateEntityPermission(context, blog);

                    blog.Topic = model.Topic;
                    blog.Text = model.Text;
                    blogs.Save();
                }
                return RedirectToAction("Index");
            }
            catch(PermissionOperationException e)
            {
                return View("Forbidden");
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
