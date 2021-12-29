using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class PersistanceManager<T> : IPersistanceManager<T>
    {
        readonly IPersistanceHandler<T> handler;
        readonly IPrincipalPersistanceProvider<T> provider;
        readonly IPrincipal principal;
        public PersistanceManager(
            IPersistanceHandler<T> handler,
        IPrincipalPersistanceProvider<T> provider, IPrincipal principal)
        {
            this.handler = handler;
            this.provider = provider;
            this.principal = principal;
        }

        public void Save(T model) => handler.Save(model);
        public T Get() => provider.Handle(principal, handler);
        public void Clear() => handler.Clear();
    }
}