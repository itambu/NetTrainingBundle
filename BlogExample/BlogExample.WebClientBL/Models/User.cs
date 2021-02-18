using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.WebClientBL.Models
{
    public class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Created { get; set; }
        public string EMail { get; set; } 
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }
    }
}
