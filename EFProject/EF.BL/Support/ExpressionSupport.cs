﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF.BL.Support
{
    public static class ExpressionSupport
    {
        public static Expression<Func<TTargetType, bool>> Project<TSourceType, TTargetType>(this Expression<Func<TSourceType, bool>> sourceExpression)
        {
            ParameterExpression sourceParameter = sourceExpression.Parameters.FirstOrDefault();
            ParameterExpression targetParameter = Expression.Parameter(typeof(TTargetType), sourceParameter.Name);
            Expression newBody = new TransformVisitor<TTargetType>(sourceParameter, targetParameter).Visit(sourceExpression.Body);
            var paramsList = sourceExpression.Parameters.ToList();
            var position = paramsList.IndexOf(sourceParameter);
            paramsList[position] = targetParameter;
            return Expression.Lambda<Func<TTargetType, bool>>(newBody, paramsList);
        }


        private class TransformVisitor<TTarget> : ExpressionVisitor
        {
            private ParameterExpression _targetParameter;
            private ParameterExpression _sourceParameter;

            public TransformVisitor(ParameterExpression sourceParameter, ParameterExpression targetParameter)
            {
                _sourceParameter = sourceParameter;
                _targetParameter = targetParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _sourceParameter ? _targetParameter : base.VisitParameter(node);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if ((node.Member.MemberType & System.Reflection.MemberTypes.Property ) != 0)
                {
                    MemberExpression newExpression = Expression.Property(Visit(node.Expression), node.Member.Name);
                    return newExpression;
                }
                else if( (node.Member.MemberType & System.Reflection.MemberTypes.Field)!=0)
                {
                    MemberExpression newExpression = Expression.Field(Visit(node.Expression), node.Member.Name);
                    return newExpression;
                }
                else
                {
                    return base.VisitMember(node);
                }
            }
        }
    }

}

