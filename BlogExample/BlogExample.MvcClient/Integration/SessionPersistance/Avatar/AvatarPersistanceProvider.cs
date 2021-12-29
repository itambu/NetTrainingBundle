using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class AvatarPersistanceProvider : EntityPrincipalProvider<AvatarViewModel>
    {
        public AvatarPersistanceProvider()
            : base(
                        new PrincipalToUserTransformation(),
                        new AvatarPrincipalVerifyer(),
                        new AvatarEqualityComparer()
                  )
        {
        }
    }
}