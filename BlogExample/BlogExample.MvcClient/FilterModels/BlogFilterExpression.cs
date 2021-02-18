using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BlogExample.MvcClient.FilterModels
{
    public class BlogFilterExpression : BaseFilter<Blog>
    {
        private BlogFilter Filter { get; set; }
        public BlogFilterExpression(BlogFilter filter)
        {
            Filter = filter;
        }
        public override Expression<Func<Blog, bool>> Query
        {
            get 
            {
                Expression<Func<Blog, bool>> expression = null;
                if (Filter.TopicSubstring != null)
                {
                    expression = expression.Combine(e => e.Topic.Contains(Filter.TopicSubstring));
                }
                if (Filter.UserSubstring != null && !Filter.OnlyMyBlog)
                {
                    expression = expression.Combine(e => e.User.Nickname.Contains(Filter.UserSubstring));
                }

                if (Filter.From.HasValue)
                {
                    expression = expression.Combine(e => e.Created >= Filter.From);
                }
                if (Filter.To.HasValue)
                {
                    expression = expression.Combine(e => e.Created <= Filter.To);
                }
                if (Filter.OnlyMyBlog)
                {
                    expression = expression.Combine(e => e.User.Id == Filter.AuthorizedUserId);
                }
                return expression;
            }
        }
    }
}