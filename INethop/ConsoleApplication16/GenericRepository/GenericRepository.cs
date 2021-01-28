using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        protected ICollection<T> Items { get; private set; }

        public GenericRepository(ICollection<T> sourceCollection)
        {
            Items = sourceCollection;
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public IEnumerable<T> Get()
        {
            return Items;
        }

        public void Remove(T item)
        {
            Items.Remove(item);
        }

        public void Save()
        {
        }
    }
}
