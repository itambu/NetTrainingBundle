using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class AppOptions
    {
        public AppFolderOptions FolderOptions { get; set; }
        public int TimeoutForStop { get; set; }
        public ConnectionOptions ConnectionOptions {get;set;}
    }
}
