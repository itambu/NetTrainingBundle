namespace Blogs.BL.Abstractions.Factories
{
    public interface IDataItemHandlerFactory<DTOEntity>
    {
        public IDataItemHandler<DTOEntity> CreateInstance();
    }
}
