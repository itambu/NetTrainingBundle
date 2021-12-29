using BlogExample.MvcClient.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.PermissionService
{
    public class PermissionConfiguration
    {
        private ICollection<object> items;
        public IEnumerable<object> Items => items;


        public PermissionConfiguration()
        {
            items = new List<object>();
        }

        public PermissionConfiguration Register<Requested, Owner>(
            Expression<Func<Requested, Owner, IPrincipal, bool>> rule,
            OperationPermission value)
        {
            items.Add(new PermissionRule<Requested, Owner>(rule, value));
            return this;
        }
    }
}