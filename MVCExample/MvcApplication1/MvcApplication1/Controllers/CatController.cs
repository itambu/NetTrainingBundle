using MvcApplication1.App_Start;
using MvcApplication1.Infrastucture;
using MvcApplication1.Models;
using MvcApplication1.Models.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CatController : Controller
    {
        //
        // GET: /Cat/

        static IList<Cat> catList = new List<Cat>()
        {
            new Cat(){ Id = 1, Name= "first"},
            new Cat(){ Id = 2, Name= "second"},
            new Cat(){ Id = 3, Name= "third"},
            new Cat(){ Id = 4, Name= "first"},
            new Cat(){ Id = 5, Name= "second"},
            new Cat(){ Id = 6, Name= "third"},
            new Cat(){ Id = 7, Name= "first"},
            new Cat(){ Id = 8, Name= "second"},
            new Cat(){ Id = 9, Name= "third"},
            new Cat(){ Id = 10, Name= "first"},
            new Cat(){ Id = 11, Name= "second"},
            new Cat(){ Id = 12, Name= "third"}
        };

        public ActionResult Index(int? page)
        {
            var config = new StorageRouter(this);

            ModelFilter<Cat> filter = config.GetObject<CatFilter>();

            if (filter == null)
            {
                filter = ModelFilterConfig.Factory.CreateInstance<Cat>();
                config.SetObject(filter);
            }
            var query = filter.ToExpression();
            var q = query != null ? catList.Where(query.Compile()) : catList;

            var lastViewPage = page ?? 1;
            config.SetObject<IPagedList<Cat>>(q.ToPagedList(lastViewPage, 10));

            Session["LastViewCatPage"] = lastViewPage;

            return View(filter);
        }


        [HttpPost]
        public ActionResult Index(CatFilter filter)
        {
            if (TryValidateModel(filter))
            {
                var config = new StorageRouter(this);
                config.SetObject(filter);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Cat/Details/5

        public ActionResult Details(int id)
        {
            try
            {
                var c = catList.SingleOrDefault(m => m.Id == id);
                 return View(c);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Cat/Create

        public ActionResult Create()
        {
            Cat c = new Cat();
            return View("Create", c);
        }

        //
        // POST: /Cat/Create

        [HttpPost]
        public ActionResult Create(Models.Cat cat)
        {
            try
            {
                // TODO: Add insert logic here
                ModelState.AddModelError("Name", "kuku");
                return View(cat);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Cat/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                var c = catList.SingleOrDefault(m => m.Id == id);
                return View(c);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Cat/Edit/5

        [HttpPost]
        public ActionResult Edit(Cat cat)
        {
            try
            {
                if (TryValidateModel(cat))
                {
                    var source = catList.SingleOrDefault(c => c.Id == cat.Id);
                    var position = catList.IndexOf(source);
                    catList[position] = cat;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cat);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Cat/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                var model = catList.SingleOrDefault(x => x.Id == id);
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Cat/Delete/5

        [HttpPost]
        public ActionResult Delete(Cat model)
        {
            try
            {
                var item = catList.SingleOrDefault(x => x.Id == model.Id);
                catList.Remove(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
