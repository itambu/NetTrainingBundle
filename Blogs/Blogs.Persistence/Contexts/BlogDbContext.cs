using Blogs.Persistence.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Text;


namespace Blogs.Persistence.Contexts
{
    public partial class BlogDbContext : DbContext 
    {
        private readonly DbConnection _connection;
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogDbContext(DbConnection connection, bool ownConnection) : base(connection, ownConnection)
        {
            _connection = connection;
            //Database.Delete();
            //Database.CreateIfNotExists();
            //Database.Create();
        }



        //public BlogDbContext(DbContextOptions options)
        //    : base(options)
        //{
        //    //Database.EnsureDeleted();
        //    //Database.EnsureCreated();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x=>x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<User>().Property(x=>x.FullName).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<User>().HasMany(x => x.Comments).WithRequired(x=>x.User);
            modelBuilder.Entity<User>().HasMany(x => x.Blogs).WithRequired(x=>x.User);
            

            modelBuilder.Entity<Blog>()
                .Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Blog>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Blog>()
                .HasKey(x=>x.Id);
            modelBuilder.Entity<Blog>()
                .HasMany(x => x.Comments).WithRequired(x=>x.Blog).WillCascadeOnDelete(false);
          

            modelBuilder.Entity<Comment>().HasKey(x=>x.Id)
                .Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>()
                .Property(x => x.Text).HasMaxLength(4000);
            modelBuilder.Entity<Comment>()
                .Property(x => x.Session).IsOptional();
            //base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (_connection != null)
        //    {
        //        optionsBuilder.UseSqlServer(_connection);
        //    }
        //        //optionsBuilder
        //        //    .UseSqlServer()
        //        //    //.UseSqlServer(ConfigurationManager.ConnectionStrings["blog_db_test"].ConnectionString)
        //        //    ;
            
        //}

    }
}
