using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class BlogFilterViewModel
    {
        [StringLength(15)]
        [Display(Name ="Topic")]
        public string TopicSubstring { get; set; }
        [StringLength(15)]
        [Display(Name = "User")]
        public string UserSubstring { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? From { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
        [Display(Name = "Only my blogs")]

        public bool OnlyMyBlog { get; set; }
        public int? AuthorizedUserId { get; set; }
    }
}