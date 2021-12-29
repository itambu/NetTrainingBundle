using BlogExample.MvcClient.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarPrincipalVerifyer : IVerifyBinding<AvatarViewModel, IPrincipal>
    {
        public bool HasBinding(AvatarViewModel model, IPrincipal principal)
        {
            return model.LoginIndentifier == principal.Identity.Name;
        }
    }
}