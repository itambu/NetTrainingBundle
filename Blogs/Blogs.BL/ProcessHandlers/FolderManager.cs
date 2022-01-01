using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;

namespace Blogs.BL.ProcessManagers
{
    public class FolderManager<DTOEntity> : BaseFileManager<DTOEntity>, IProcessHandler<DTOEntity>, ISyncStart
    {
        protected IEnumerable<IBlogDataSource<DTOEntity>> Provider { get; set; }

        public FolderManager(
            IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
            IEnumerable<IBlogDataSource<DTOEntity>> provider,
            ActionTokenSet tokens
            ) : base(dataSourceHandleBuilder, tokens)
        {
            Provider = provider;
        }

        public virtual void Start()
        {
            StartProcess(PendingTask);
        }

        public override void StartProcess(Action<IBlogDataSource<DTOEntity>> pendingTask)
        {
            foreach (var c in Provider)
            {
                if (Tokens.Cancel.IsCancellationRequested || Tokens.Cancel.IsCancellationRequested)
                {
                    break;
                }
                pendingTask(c);
            }
        }
    }
}
