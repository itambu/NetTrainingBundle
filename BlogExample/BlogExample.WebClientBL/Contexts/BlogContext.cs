using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.WebClientBL.Contexts
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);
        //}

        protected void Initialize()
        {
            //Database.SetInitializer<BlogContext>(new DropCreateDatabaseAlways<BlogContext>());
        }

        public BlogContext() : base("name=webclient_blog")
        {
            Initialize();
            Database.Log = (s) => {
                using (var stream = new StreamWriter(@"d:\trace.txt", true))
                {
                    stream.WriteLine(s);
                }
            };
        }

        public BlogContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
            Initialize();
        }
    }
}
