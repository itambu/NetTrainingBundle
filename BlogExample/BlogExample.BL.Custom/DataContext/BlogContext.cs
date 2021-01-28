using BlogExample.BL.Custom.DataContext.Configurations;
using BlogExample.DAL.Contexts;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.DataContext
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new BlogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected void Initialize()
        {
            //Database.SetInitializer<BlogContext>(new DropCreateDatabaseAlways<BlogContext>());
        }

        public BlogContext() : base("name=blogdb")
        {
            Initialize();
        }

        public BlogContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
            Initialize();
        }
    }
}
