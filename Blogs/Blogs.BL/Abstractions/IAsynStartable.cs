﻿using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncStartable
    {
        Task Start();
    }
}
