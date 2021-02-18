using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class BlogDetailViewModel : BlogSimpleViewModel
    {
        [Required]
        [StringLength(30000, MinimumLength = 3)]
        public string Text { get; set; }

        public IPagedList<CommentViewModel> Comments { get; set; }
    }
}