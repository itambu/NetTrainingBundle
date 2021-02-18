using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class CreateBlogViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Topic { get; set; }

        [Required]
        [StringLength(30000, MinimumLength = 3)]
        public string Text { get; set; }
    }
}