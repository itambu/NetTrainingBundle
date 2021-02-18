using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.FilterModels
{
    public class BlogFilter
    {
        public string TopicSubstring { get; set; }
        public string UserSubstring { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int AuthorizedUserId { get; set; }
        public bool OnlyMyBlog { get; set; }
    }
}