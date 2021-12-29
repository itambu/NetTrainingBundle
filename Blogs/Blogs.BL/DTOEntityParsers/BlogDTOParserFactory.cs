using Blogs.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
