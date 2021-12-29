using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IConnectionFactory
    {
        DbConnection CreateInstance(bool openOnCreate = false);
    }
}
