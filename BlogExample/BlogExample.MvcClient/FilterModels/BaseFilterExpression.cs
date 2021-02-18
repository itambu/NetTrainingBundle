using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BlogExample.MvcClient.FilterModels
{
    public abstract class BaseFilter<Model>
    {
        public abstract Expression<Func<Model, bool>> Query { get; }
    }
}