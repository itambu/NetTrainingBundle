using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncAdapter<DTOEntity> : IAsyncStartable, IDataSourceProceedable<DTOEntity>
    {
        Task WhenAll();
        Task WhenMainProcess();
    }
}
