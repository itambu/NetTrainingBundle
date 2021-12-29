using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PersistanceService
{
    public abstract class BasePersistanceHandler<T> : IPersistanceHandler<T>
    {
        protected Action<T> SaveHandler { get; private set; }
        protected Action ClearHandler { get; private set; }
        protected Func<T> GetHandler { get; private set; }

        public BasePersistanceHandler(Func<T> loadHandler, Action<T> saveHandler, Action clearHandler)
        {
            SaveHandler = saveHandler;
            GetHandler = loadHandler;
            ClearHandler = clearHandler;
        }

        public void Save(T model)
        {
            SaveHandler.Invoke(model);
        }

        public T Get()
        {
            return GetHandler.Invoke();
        }

        public void Clear()
        {
            ClearHandler.Invoke();
        }
    }
}