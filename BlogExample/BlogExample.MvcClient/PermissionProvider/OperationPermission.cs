using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    [Flags]
    public enum OperationPermission
    {
        None = 0,
        Get = 1,
        Update = 2,
        Create = 4,
        Delete = 8,
        All = 15
    }
}