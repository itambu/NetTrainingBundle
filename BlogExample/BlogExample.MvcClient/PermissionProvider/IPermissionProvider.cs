using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    public interface IPermissionProvider
    {
        bool HasPermission<RequstedEntity, PermissionOwnerEntity>(
            RequstedEntity requsted,
            PermissionOwnerEntity owner,
            IPrincipal principal,
            OperationPermission requestedPermission);
    }
}