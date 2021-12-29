using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public class UserToAvatarTransformation : ITransformation<User,IPrincipal, AvatarViewModel>
    {
        public AvatarViewModel Transform(User entity, IPrincipal principal)
        {
            return new AvatarViewModel()
            {
                Id = entity.Id,
                Nickname = entity.Nickname,
                LoginIndentifier = principal.Identity.Name
            };
        }
    }
}