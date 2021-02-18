using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class BlogHomeViewModel : BlogSimpleViewModel
    {
        public CommentViewModel LastComment { get; set; }
    }
}