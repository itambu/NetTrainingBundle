using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.Model.Models
{
    public class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
        }
        public int Id { get; set; }
        public string Nickname { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
