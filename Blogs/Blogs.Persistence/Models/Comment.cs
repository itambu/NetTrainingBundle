using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Persistence.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Blog Blog { get; set; }
        public string Text { get; set; }
        public Guid? Session { get; set; }
    }
}
