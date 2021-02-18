using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BlogExample.MvcClient.FilterModels
{
    public static class ExpressionHelper
    {
        private class ReplaceExpressionVisitor
        : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }

        public static Expression<Func<ViewModel, bool>> Combine<ViewModel>(
            this Expression<Func<ViewModel, bool>> source,
            Expression<Func<ViewModel, bool>> follow
            )
        {
            if (source==null)
            {
                return follow;
            }

            var param = source.Parameters;
            if (follow!=null)
            {
                //Expression<Func<ViewModel, bool>> _newFollow = LambdaExpression.Lambda<Func<ViewModel, bool>>(follow.Body, param);
                ReplaceExpressionVisitor visitor = new ReplaceExpressionVisitor(follow.Parameters[0], source.Parameters[0]);
                Expression _newFollow = visitor.Visit(follow.Body);
                return LambdaExpression.Lambda<Func<ViewModel, bool>>(LambdaExpression.And(source.Body, _newFollow), param);
            }
            return source;
        }

    }
}