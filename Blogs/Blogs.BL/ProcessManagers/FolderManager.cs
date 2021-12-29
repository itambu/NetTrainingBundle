using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.BL.DataItemHandlers;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.ProcessManagers
{
    public class FolderManager<DTOEntity> : BaseFileManager<DTOEntity>, IFileProcessManager<DTOEntity>
    {
        protected IEnumerable<IBlogDataSource<DTOEntity>> Provider { get; set; }
        
        public FolderManager(
            IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
            IEnumerable<IBlogDataSource<DTOEntity>> provider,
            IParallelismHandler parallelismHandler
            ) : base(dataSourceHandleBuilder, parallelismHandler)
        {
            Provider = provider;
        }

        public virtual Task Run()
        {
            Task temp = Task.Factory.StartNew(() =>
            {
                foreach (var c in Provider)
                {
                    if (ParallelismHandler.StopTokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                    ParallelismHandler.Add(CreateTask(c));
                }
            });
            //ParallelismHandler.Add(temp);
            return temp;
        }
    }
}
