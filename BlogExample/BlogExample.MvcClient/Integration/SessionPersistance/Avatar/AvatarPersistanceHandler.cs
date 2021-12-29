using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarPersistanceHandler 
        : SessionPersistanceHandler<AvatarViewModel>
    {
        public AvatarPersistanceHandler(
            HttpSessionStateBase session)
            : base(session, "SessionPersistanceUserViewModel")
        {
        }
    }
}