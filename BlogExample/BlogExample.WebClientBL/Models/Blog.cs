using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.WebClientBL.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Blog()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
