using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class AddCommentViewModel
    {
        public int BlogId { get; set; }
        public string Text { get; set; }
    }
}