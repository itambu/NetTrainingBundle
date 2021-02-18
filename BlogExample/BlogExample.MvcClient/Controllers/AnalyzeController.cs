using BlogExample.WebClientBL.Contexts;
using BlogExample.DAL.Repositories;
using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BlogExample.MvcClient.Controllers
{
    public class AnalyzeController : Controller
    {
        // GET: Analyze
        [Authorize(Roles ="Admins")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        protected bool Validate(CommentPerDayFilterViewModel model)
        {
            bool result = true;
            if (model.Start.Date > model.Finish.Date)
            {
                result = false;
                ModelState.AddModelError("finish", "must be greater or equal than start");
            }

            if ((model.Finish.Date - model.Start.Date).TotalDays > 30)
            {
                result = false;
                ModelState.AddModelError("finish", "time interval should be less than 30 days");
            }

            return result;
        }

        [Authorize(Roles = "Admins")]
        [HttpGet]
        public ActionResult CommentPerDay(CommentPerDayFilterViewModel model)
        {
            CommentPerDayFilterViewModel workModel 
            = new CommentPerDayFilterViewModel() 
                { 
                    Start = DateTime.Now, 
                    Finish = DateTime.Now
                };
            SaveToViewBagCommentPerDay(workModel);

            return View("CommentPerDay", workModel);
        }

        protected void SaveToViewBagCommentPerDay(CommentPerDayFilterViewModel model)
        {
            using (var context = new BlogContext())
            {
                var query = new GenericRepository<Comment>(context)
                    .Get(x => DbFunctions.TruncateTime(x.Created) >= DbFunctions.TruncateTime(model.Start)
                            && DbFunctions.TruncateTime(x.Created) <= DbFunctions.TruncateTime(model.Finish))
                    .GroupBy(x => DbFunctions.TruncateTime(x.Created))
                    .Select(x => new { Label = x.Key.Value, Count = x.Count() })
                    .ToArray();
                ViewBag.Labels = String.Join(",", query.Select(x => String.Concat("'", x.Label.ToShortDateString(), "'")).ToArray());
                ViewBag.Values = String.Join(",", query.Select(x => x.Count.ToString()).ToArray());
            }
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public ActionResult ApplyFilter(CommentPerDayFilterViewModel model)
        {
            Validate(model);
            if (ModelState.IsValid)
            {
                SaveToViewBagCommentPerDay(model);
            }

            return View("CommentPerDay", model);
        }
    }
}