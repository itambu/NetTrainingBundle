using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class BlogIndexViewModel
    {
        public BlogFilterViewModel FilterModel { get; set; }
        public IPagedList<BlogSimpleViewModel> Items { get; set; }
    }
}