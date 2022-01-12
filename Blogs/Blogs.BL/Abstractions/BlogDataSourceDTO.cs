using System;

namespace Blogs.BL.Abstractions
{
    public class BlogDataSourceDTO
    {
        public string UserName { get; set; }
        public string BlogName { get; set; }
        public string CommentTopic { get; set; }
        public Guid Session { get; set; }
    }
}
