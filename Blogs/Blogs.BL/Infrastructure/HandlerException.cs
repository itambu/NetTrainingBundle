using System;

namespace Blogs.BL.Infrastructure
{
    public class HandlerException : Exception
    {
        public HandlerException(Exception inner) : base("Data source hadler exception has occured", inner)
        {
        }
    }
}
