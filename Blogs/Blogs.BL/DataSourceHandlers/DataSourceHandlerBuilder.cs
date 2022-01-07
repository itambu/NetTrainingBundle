using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.BusinessLogicUoWs;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System.Data.Entity;
using System.Threading;

namespace Blogs.BL.DataSourceHandlers
{
    //public class DataSourceHandlerBuilder<DTOEntity> : IDataSourceHandleBuilder<DTOEntity>
    //{
    //    protected IDataItemHandlerFactory<DTOEntity> _dataItemHandlerFactory;
    //    public virtual IDataSourceHandlerFactory<DTOEntity> HandlerFactory { protected get; set; }
    //    public virtual CancellationToken CancelToken { protected get; set; }

    //    public IDataSourceHandler Build(IDataSource<DTOEntity> source)
    //    {
    //        return HandlerFactory.CreateInstance(
    //            source, 
    //            _dataItemHandlerFactory.CreateInstance(), 
    //            CancelToken);
    //    }
    //}
}
