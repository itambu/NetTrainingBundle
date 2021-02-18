using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BlogExample.MvcClient.FilterModels
{
    public class CommentPerDayFilterProjector : BaseFilter<Comment>
    {
        private CommentPerDayFilterViewModel Filter;

        public CommentPerDayFilterProjector(CommentPerDayFilterViewModel filter)
        {
            Filter = filter;
        }
        public override Expression<Func<Comment, bool>> Query
        {
            get
            {
                Expression<Func<Comment, bool>> expression = null;
                return expression = expression
                    .Combine(e => e.Created >= Filter.Start)
                    .Combine(e => e.Created <= Filter.Finish);
            }
        }
    }
}