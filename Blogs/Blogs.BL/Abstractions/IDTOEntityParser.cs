using Blogs.Persistence.Models;

namespace Blogs.BL.Abstractions
{
    public interface IDTOEntityParser<DTOEntity>
    {
        User User { get; set; }
        Blog Blog { get; set; }
        Comment Comment { get; set; }
        void Parse(DTOEntity item);
    }
}
