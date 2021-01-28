using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16.GenericRepository
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Get();
        void Add(T item);
        void Remove(T item);
        void Save();
    }
}
