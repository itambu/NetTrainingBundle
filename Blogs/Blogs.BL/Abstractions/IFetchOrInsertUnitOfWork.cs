using System;
using System.Linq.Expressions;

namespace Blogs.BL.Abstractions
{
    public interface IFetchOrInsertUnitOfWork<Entity> : ISingleEntityUoW<Entity> where Entity : class
    {
        Entity PerformAction(Expression<Func<Entity, bool>> filter, Entity forInsert);
    }
}
