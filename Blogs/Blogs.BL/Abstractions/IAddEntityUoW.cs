using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAddEntityUoW<Entity> : ISingleEntityUoW<Entity> where Entity : class
    {
        void PerformAction(Entity item);
    }
}
