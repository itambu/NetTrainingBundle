using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class KeyPersistanceHandler<T> : BasePersistanceHandler<T>
    {
        protected readonly string key;

        public KeyPersistanceHandler(Func<T> loader, Action<T> saver, Action clearer, string key)
            : base(loader, saver, clearer)
        {
            this.key = key;
        }
    }
}