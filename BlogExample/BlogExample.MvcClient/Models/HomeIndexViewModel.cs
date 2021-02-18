using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class HomeIndexViewModel
    {
        IEnumerable<Blog> LastCreatedBlogs { get; set; }
        //IEnumerable<>
    }
}