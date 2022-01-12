namespace Blogs.BL.Abstractions
{
    public interface IDataSourceFactory<DTOEntity>
    {
        IDataSource<DTOEntity> CreateInstance(string fileName);
    }
}
