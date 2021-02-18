using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class CommentViewModel : AddCommentViewModel
    {
        public int Id { get; set; }
        public UserViewModel Commenter { get; set; }
        public DateTime Created { get; set; }

    }
}