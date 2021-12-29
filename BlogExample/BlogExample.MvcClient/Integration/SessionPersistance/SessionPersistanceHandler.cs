using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class SessionPersistanceHandler<T> : KeyPersistanceHandler<T>
    {
        public SessionPersistanceHandler(HttpSessionStateBase session, string key)
            : base(
                  ()=>(T)session[key], 
                  x=>session[key] = x,
                  ()=> { session[key] = default(T); },
                  key)
        {
        }
    }
}