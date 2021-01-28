using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16.Interfaces.ObservableList
{
    public class ObservableList<T> : List<T>, ICollection<T>
    {
        public event EventHandler<T> AddEvent;
        public void Add(T item)
        {
            base.Add(item);
            OnAdd(this, item);
        }

        protected virtual void OnAdd(object sender, T item)
        {
            if (AddEvent != null)
            {
                AddEvent(sender, item);
            }
        }
    }
}
