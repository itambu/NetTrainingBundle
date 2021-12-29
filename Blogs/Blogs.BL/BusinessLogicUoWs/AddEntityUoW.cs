using Blogs.BL.Abstractions;
using Blogs.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.BusinessLogicUoWs
{
    public class AddEntityUoW<Entity> : BaseUoW<Entity>, IAddEntityUoW<Entity> where Entity : class
    {
        public void PerformAction(Entity item)
        {
            try
            {
                Repository.Add(item);
                Repository.Context.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }

        public AddEntityUoW(IGenericRepository<Entity> repo) : base(repo)
        {
        }
    }
}
