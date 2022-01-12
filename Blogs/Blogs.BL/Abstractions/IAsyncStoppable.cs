using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncStoppable
    {
        Task Stop();
    }
}
