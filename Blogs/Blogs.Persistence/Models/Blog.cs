using System.Collections.Generic;

namespace Blogs.Persistence.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Blog()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
