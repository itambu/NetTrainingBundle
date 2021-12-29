using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.BaseHandlers
{
    public class HandlerException : Exception
    {
        public HandlerException(Exception inner) : base("Handler exception",  inner)
        {

        }
    }
}
