namespace Blogs.BL.Abstractions
{
    public interface IDTOParserFactory<DTOEntity>
    {
        IDTOEntityParser<DTOEntity> CreateInstance();
    }
}
