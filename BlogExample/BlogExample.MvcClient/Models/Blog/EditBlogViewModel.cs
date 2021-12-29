using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogExample.MvcClient.Models
{
    public class EditBlogViewModel : CreateBlogViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Created { get; set; }
        public UserViewModel Author { get; set; }
    }
}