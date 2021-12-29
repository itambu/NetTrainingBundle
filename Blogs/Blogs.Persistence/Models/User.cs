using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Persistence.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; private set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

        public User()
        {
            Comments = new HashSet<Comment>();
            Blogs = new HashSet<Blog>();
        }
    }
}
