using BlogExample.MvcClient.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    public class PermissionProvider : IPermissionProvider
    {
        private PermissionConfiguration config;
        public PermissionProvider(PermissionConfiguration config)
        {
            this.config = config;
        }

        public bool HasPermission<RequstedEntity, PermissionOwnerEntity>(
            RequstedEntity requsted,
            PermissionOwnerEntity owner,
            IPrincipal principal, 
            OperationPermission requestedPermission)
        {
            return (config.Items
                .OfType<PermissionRule<RequstedEntity, PermissionOwnerEntity>>() 
                .Where(x=>x.Rule.Compile().Invoke(requsted, owner, principal))
                .Aggregate(OperationPermission.None, (x, s) => x | s.Value)
                     & requestedPermission) != OperationPermission.None;
        }
    }
}