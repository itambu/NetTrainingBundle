using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.Model.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public virtual User User { get; set; }
        public Guid? Session { get; set; }
    }
}
