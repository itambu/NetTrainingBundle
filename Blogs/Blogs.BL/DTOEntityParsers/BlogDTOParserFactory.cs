using Blogs.BL.Abstractions;

namespace Blogs.BL.DTOEntityParsers
{
    public class BlogDTOParserFactory : IDTOParserFactory<BlogDataSourceDTO>
    {
        public IDTOEntityParser<BlogDataSourceDTO> CreateInstance()
        {
            return new DTOParser();
        }
    }
}
