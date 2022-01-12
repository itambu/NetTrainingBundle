using Blogs.BL.Abstractions;

namespace Blogs.BL.AsyncAdapters
{
    public class AsyncAdapterFactory<DTOEntity>
    {
        public IAsyncAdapter<DTOEntity> CreateInstance(
            IDataSourceHandlerAdapter<DTOEntity> processHandler,
            AsyncAdapterOptions options,
            ISyncStartable startableInstance)
        {
            return new BaseAsyncAdapter<DTOEntity>(processHandler, options, startableInstance);
        }
    }
}
