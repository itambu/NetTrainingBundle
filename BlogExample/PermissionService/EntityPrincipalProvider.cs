using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{
    public class EntityPrincipalProvider<
        T> : IPrincipalPersistanceProvider<T>
    {
        private ITransformation<IPrincipal, T> TransformToViewModel { get; set; }

        private IVerifyBinding<T, IPrincipal> Verifyer { get; set; }

        private IEqualityComparer<T> EqulityComparer { get; set; }

        public EntityPrincipalProvider(
            ITransformation<IPrincipal, T> transformToViewModel,
            IVerifyBinding<T, IPrincipal> verifyer,
            IEqualityComparer<T> equlityComparer
            )
        {
            TransformToViewModel = transformToViewModel;
            Verifyer = verifyer;
            EqulityComparer = equlityComparer;
        }

        protected virtual T Get(IPrincipal principal, IPersistanceHandler<T> persistanceHandler)
        {
            return (principal.Identity.IsAuthenticated) 
                ? persistanceHandler.Get() : default(T);
        }

        protected virtual bool Verify(IPrincipal principal, T model)
        {
            return !EqulityComparer.Equals(model, default(T)) && Verifyer.HasBinding(model, principal);
        }


        protected virtual T Transform( IPrincipal principal)
        {
            return TransformToViewModel.Transform(principal);
        }

        public T Handle(IPrincipal principal, 
            IPersistanceHandler<T> persistanceHandler)
        { 
            try
            {
                var model = Get(principal, persistanceHandler);
                return Verify(principal, model) ? model : 
                    Transform(principal);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Authorized user wasn't bind to domain user", e);
            }
        }
    }
}
