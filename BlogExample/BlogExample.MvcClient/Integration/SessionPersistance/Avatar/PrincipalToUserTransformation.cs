using BlogExample.DAL.Contexts;
using BlogExample.MvcClient.Models;
using BlogExample.WebClientBL.Models;
using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance.Avatar
{
    public class PrincipalToUserTransformation : ITransformation<IPrincipal, AvatarViewModel>
    {
        private readonly IDbContextFactory factory;
        public AvatarViewModel Transform(IPrincipal principal)
        {
            using (var context = factory.CreateInstance())
            {
                var entity = context.Set<User>().Single(x => x.EMail == principal.Identity.Name);
                return new AvatarViewModel()
                {
                    Id = entity.Id,
                    Nickname = entity.Nickname,
                    LoginIndentifier = principal.Identity.Name
                };
            }
        }

        public PrincipalToUserTransformation(IDbContextFactory factory)
        {
            this.factory = factory;
        }

        public PrincipalToUserTransformation() : this(ServiceLocatorHelper.Locator.Get<IDbContextFactory>())
        {
        }

    }
}