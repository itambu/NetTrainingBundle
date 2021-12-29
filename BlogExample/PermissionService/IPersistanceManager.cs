using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersistanceService
{
    public interface IPersistanceManager<T>
    {
        void Save(T model);
        T Get();
        void Clear();

    }
}