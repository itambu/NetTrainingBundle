using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.Entities.Classes
{
    public class HistoryItem<Entity, STATUS>
    {
        public int Id { get; private set; }
        public Entity Entity { get; private set; }
        public DateTime Changed { get; private set; }
        public STATUS Status { get; private set; }

        public HistoryItem(int id, Entity entity, DateTime changed, STATUS status)
        {
            Id = id;
            this.Entity = entity;
            Changed = changed;
            Status = status;
        }
    }
}
