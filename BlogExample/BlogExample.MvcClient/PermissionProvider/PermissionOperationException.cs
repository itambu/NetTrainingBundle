using BlogExample.MvcClient.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    public class PermissionOperationException : Exception
    {
        public PermissionOperationException(string entityName)
        : base($"Forbidden operation for user {entityName}")
        {
        }
    }
}