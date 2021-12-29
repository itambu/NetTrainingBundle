using AutoMapper;
using BlogExample.MvcClient.FilterModels;
using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Controllers
{
    public class BlogFilterController : ServiceLocatorController
    {

        public ActionResult Index()
        {
            BlogFilterViewModel model = Locator.Get<IMapper>()
                .Map<BlogFilter, BlogFilterViewModel>(
                Locator.Get<IParametrizedFactory<IPersistanceManager<BlogFilter>, HttpContextBase>>().CreateInstance(HttpContext).Get());
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
                Locator.Get<IParametrizedFactory<IPersistanceManager<BlogFilter>, HttpContextBase>>()
                    .CreateInstance(HttpContext)
                    .Save(Locator.Get<IMapper>().Map<BlogFilter>(model));
            }
            return PartialView("Index", model);
        }
    }
}