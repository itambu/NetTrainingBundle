using BlogExample.MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarEqualityComparer : IEqualityComparer<AvatarViewModel>
    {
        public bool Equals(AvatarViewModel x, AvatarViewModel y)
        {
            return Object.ReferenceEquals(x, y);
        }

        public int GetHashCode(AvatarViewModel obj)
        {
            return obj != null ? obj.Id : 0;
        }
    }
}