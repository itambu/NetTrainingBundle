using Blogs.BL.Abstractions;
using Blogs.Persistence.Models;
using System;

namespace Blogs.BL.DTOEntityParsers
{
    public class DTOParser : IDTOEntityParser<BlogDataSourceDTO>
    {
        public User User { get; set; }
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }

        public void Parse(BlogDataSourceDTO item)
        {
            var name = item.UserName.Split(" ");

            if (name.Length != 2)
            {
                throw new InvalidOperationException("Cannot split username");
            }

            User = new User() { FirstName = name[0], LastName = name[1] };
            Blog = new Blog() { User = User, Name = item.BlogName };
            Comment = new Comment() { Blog = Blog, User = User, Text = item.CommentTopic, Session = item.Session };
        }
    }
}
