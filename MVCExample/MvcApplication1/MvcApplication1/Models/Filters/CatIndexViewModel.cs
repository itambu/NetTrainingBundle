using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.Filters
{
    public class CatIndexViewModel
    {
        ModelFilter<Cat> Filter { get; set; }
        IPagedList<Cat>  Items { get; set; }
        int PageSize { get; set; }
    }
}