namespace Blogs.BL.Abstractions
{
    public interface IDataSourceHandlerFactory<DTOEntity>
    {
        IDataSourceHandler CreateInstance(IDataSource<DTOEntity> source);
    }
}
