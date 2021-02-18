using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Contexts;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        [HttpPost]
        public ActionResult Create(AddCommentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new BlogContext())
                    {
                        var comment = new Comment()
                        {
                            Text = model.Text,
                            User = this.GetAuthorizedUser(context),
                            Blog = (new GenericRepository<Blog>(context)).SingleOrDefault(x => x.Id == model.BlogId),
                            Created = DateTime.Now,
                        };
                        var comments = new GenericRepository<Comment>(context);
                        comments.Add(comment);
                        comments.Save();
                    }
                    if (Request.UrlReferrer != null)
                    {
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    else
                    {
                        return RedirectToAction("Details","Blog", new { id = model.BlogId, page = 1 });
                    }
                }
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}