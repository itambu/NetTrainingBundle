using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;

namespace Blogs.BL.ProcessManagers
{
    public class FolderManager<DTOEntity> : BaseFileManager<DTOEntity>, IProcessHandler<DTOEntity>, ISyncStartable
    {
        protected IEnumerable<IDataSource<DTOEntity>> Provider { get; set; }

        public FolderManager(
            IDataSourceHandlerFactory<DTOEntity> dataSourceHandlerFactory,
            IEnumerable<IDataSource<DTOEntity>> provider,
            ActionTokenSet tokens
            ) : base(dataSourceHandlerFactory, tokens)
        {
            Provider = provider;
        }

        public virtual void Start()
        {
            StartProcess(PendingTask);
        }

        public override void StartProcess(Action<IDataSource<DTOEntity>> pendingTask)
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
