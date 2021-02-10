using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcApplication1.Models.Filters
{
    public abstract class ModelFilter<ViewModel>
    {
        public abstract Expression<Func<ViewModel, bool>> ToExpression();
    }
}