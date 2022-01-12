using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncCancelable
    {
        Task Cancel();
    }
}
