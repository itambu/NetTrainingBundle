using System;

namespace Blogs.BL.Abstractions
{
    public interface IConsistencyHandler
    {
        bool IsConsisted { get; }
        void Commit(Guid session);
        void Rollback(Guid session);
        void RollbackAll();
    }
}
