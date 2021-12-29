using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
