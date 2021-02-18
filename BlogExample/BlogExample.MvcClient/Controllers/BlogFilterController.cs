using BlogExample.MvcClient.FilterModels;
using BlogExample.MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class BlogFilterController : Controller
    {

        public ActionResult Index()
        {
            BlogFilterViewModel model = MapperHelper.Mapper
                .Map<BlogFilter, BlogFilterViewModel>(this.LoadBlogFilterFromSessionOrDefault());
            return PartialView("Index", model);
        }

        protected bool Validate(BlogFilterViewModel model)
        {
            if (model.From > model.To)
            {
                ModelState.AddModelError("From", "shoud be less or equal than to date");
                return false;
            }
            return true;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Apply(BlogFilterViewModel model)
        {
            if (ModelState.IsValid && Validate(model))
            {
                if (!model.AuthorizedUserId.HasValue)
                {
                    ModelState.AddModelError("Filter", "Corrupted filter");
                }
                this.SaveBlogFilterInSession(MapperHelper.Mapper.Map<BlogFilter>(model));
            }
            return PartialView("Index", model);
        }
    }
}