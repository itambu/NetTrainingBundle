using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public class BlogDataSourceDTO
    {
        public string UserName{ get; set; }
        public string BlogName { get; set; }
        public string CommentTopic { get; set; }
        public Guid Session { get; set; }
    }
}
