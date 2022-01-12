namespace Blogs.BL.Abstractions
{
    public interface IConsistancyHandlerFactory
    {
        IConsistencyHandler CreateInstance();
    }
}
