using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    public struct PermissionRule<RequstedEntity, PermissionOwnerEntity>
    {
        public Type Requested { get; set; }
        public Type Owner { get; set; }
        public Expression<Func<RequstedEntity, PermissionOwnerEntity, IPrincipal ,bool>> Rule { get; set; }
        public OperationPermission Value { get; set; }

        public PermissionRule(
            Expression<Func<RequstedEntity, PermissionOwnerEntity, IPrincipal, bool>> rule,
            OperationPermission value)
        {
            Requested = typeof(RequstedEntity);
            Owner = typeof(PermissionOwnerEntity);
            Rule = rule;
            Value = value;
        }
    }
}